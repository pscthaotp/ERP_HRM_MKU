using System;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Model.Core;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Xpo;
using System.Collections.Generic;

namespace ERP.Module.Win.Editors.NhanSu.NhanViens
{
    public class CategorizedListEditor_GiangVienThinhGiang : GridListEditor, IComplexListEditor
    {
        //
        private XafApplication application;
        private CollectionSourceBase categoriesDataSource;
        private ListView categoriesListView;
        private CategorizedListEditorDataSource categorizedListEditorDataSource;
        private object categoryKey;
        public const string CategoryPropertyName = "Category";
        private string criteriaPropertyName;
        private CategorizedListEditorTypeDescriptionProvider descriptorProvider;
        private bool isCriteriaUpdating;
        private CollectionSourceBase itemsDataSource;
        private LayoutManager layoutManager;
        private Locker locker;
        private const string UpdateCriteriaMethodName = "UpdateCriteria";

        // Methods
        public CategorizedListEditor_GiangVienThinhGiang(IModelListView info)
            : base(info)
        {
            locker = new Locker();
            locker.LockedChanged += locker_LockedChanged;
        }

        protected override void AssignDataSourceToControl(object dataSource)
        {
            if (dataSource != null)
            {
                categorizedListEditorDataSource = new CategorizedListEditorDataSource(dataSource, base.ObjectType);
            }
            else
            {
                categorizedListEditorDataSource = null;
            }
            if (ItemsDataSource.ObjectTypeInfo.Implements<IVariablePropertiesCategorizedItem>() && (CategoriesListView != null))
            {
                IPropertyDescriptorContainer currentObject = (IPropertyDescriptorContainer)CategoriesListView.CurrentObject;
                if (currentObject != null)
                {
                    descriptorProvider.Setup(currentObject.PropertyDescriptors);
                }
            }
            base.AssignDataSourceToControl(categorizedListEditorDataSource);
        }

        private void categoriesListView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateCriteria();
            UpdateColumns();
        }

        protected override object CreateControlsCore()
        {
            if (layoutManager == null)
            {
                layoutManager = application.CreateLayoutManager(true);
                categoriesListView = application.CreateListView(ModelListViewNodesGenerator.GetListViewId(categoriesDataSource.ObjectTypeInfo.Type), categoriesDataSource, false);
                categoriesListView.SelectionChanged += categoriesListView_SelectionChanged;
                ViewItemsCollection detailViewItems = new ViewItemsCollection();
                categoriesListView.CreateControls();
                detailViewItems.Add(new ControlDetailItem("1", categoriesListView.Control));
                detailViewItems.Add(new ControlDetailItem("2", base.CreateControlsCore()));
                categoriesListView.Caption = "Category";
                layoutManager.LayoutControls(base.Model.SplitLayout, detailViewItems);
                SubscribeToTreeList();
            }
            return layoutManager.Container;
        }

        public override void Dispose()
        {
            try
            {
                UnsubscribeFromTreeList();
                if (descriptorProvider != null)
                {
                    TypeDescriptor.RemoveProvider(descriptorProvider, descriptorProvider.objectType);
                    descriptorProvider = null;
                }
                if (layoutManager != null)
                {
                    layoutManager.Dispose();
                    layoutManager = null;
                }
                if (categoriesListView != null)
                {
                    categoriesListView.SelectionChanged -= categoriesListView_SelectionChanged;
                    categoriesListView.Dispose();
                    categoriesListView = null;
                }
                if (categoriesDataSource != null)
                {
                    categoriesDataSource.Dispose();
                    categoriesDataSource = null;
                }
                if (itemsDataSource != null)
                {
                    itemsDataSource.ObjectSpace.Reloaded -= ObjectSpace_Reloaded;
                    itemsDataSource = null;
                }
                locker.LockedChanged -= locker_LockedChanged;
                application = null;
            }
            finally
            {
                base.Dispose();
            }
        }

        private void locker_LockedChanged(object sender, LockedChangedEventArgs e)
        {
            if (!e.Locked && e.PendingCalls.Contains("UpdateCriteria"))
            {
                UpdateCriteria();
            }
        }

        private void ObjectSpace_Reloaded(object sender, EventArgs e)
        {
            UpdateCriteria();
        }

        private void objectTreeList_NodesReloading(object sender, EventArgs e)
        {
            locker.Lock();
        }

        public override void SaveModel()
        {
            base.SaveModel();
            if (layoutManager != null)
            {
                layoutManager.SaveModel();
            }
        }

        public override void Setup(CollectionSourceBase collectionSource, XafApplication application)
        {
            base.Setup(collectionSource, application);
            this.application = application;
            itemsDataSource = collectionSource;
            //Lấy danh sách tất cả nhân viên
            if (collectionSource.ObjectTypeInfo.Type == typeof(GiangVienThinhGiang))
            {
                itemsDataSource.Criteria["Filter1"] = CriteriaOperator.Parse("BoPhan.Oid=?", "77b146d0-a422-4ed7-b68c-ef2a505cd02f");
            }

            itemsDataSource.ObjectSpace.Reloaded += ObjectSpace_Reloaded;
            Type memberType = collectionSource.ObjectTypeInfo.FindMember("Category").MemberType;
            
            //Lấy danh sách tất cả bộ phận
            categoriesDataSource = application.CreateCollectionSource(collectionSource.ObjectSpace, memberType, application.FindListViewId(memberType));
            
            //Lọc danh sách bộ phận chỉ lấy những bộ phận còn hoạt động
            categoriesDataSource.Criteria["Filter2"] = CriteriaOperator.Parse("NgungHoatDong=false");
            //
            if (memberType == typeof(BoPhan))
            {
                //
                categoriesDataSource.Criteria["Filter2"] = new InOperator("Oid", Common.Department_GetRoledDepartmentList_ByCurrentUser());
            }

            criteriaPropertyName = "Category." + collectionSource.ObjectSpace.GetKeyPropertyName(memberType);
            descriptorProvider = new CategorizedListEditorTypeDescriptionProvider(collectionSource.ObjectTypeInfo.Type);
            TypeDescriptor.AddProvider(descriptorProvider, collectionSource.ObjectTypeInfo.Type);
        }

        private void SubscribeToTreeList()
        {
            if (categoriesListView != null)
            {
                ObjectTreeList control = categoriesListView.Editor.Control as ObjectTreeList;
                if (control != null)
                {
                    control.NodesReloading += objectTreeList_NodesReloading;
                }
            }
        }

        private void UnsubscribeFromTreeList()
        {
            if (categoriesListView != null)
            {
                ObjectTreeList control = categoriesListView.Editor.Control as ObjectTreeList;
                if (control != null)
                {
                    control.NodesReloading -= objectTreeList_NodesReloading;
                }
            }
        }

        private void UpdateColumns()
        {
            if (typeof(IVariablePropertiesCategorizedItem).IsAssignableFrom(ItemsDataSource.ObjectTypeInfo.Type) && (CategoriesListView.CurrentObject != null))
            {
                IPropertyDescriptorContainer currentObject = (IPropertyDescriptorContainer)CategoriesListView.CurrentObject;
                object keyValue = CategoriesDataSource.ObjectSpace.GetKeyValue(currentObject);
                if (categoryKey != keyValue)
                {
                    SaveModel();
                    FocusedObject = null;
                    descriptorProvider.Setup(currentObject.PropertyDescriptors);
                    string id = String.Format("{0}_{1}_ListView", ItemClassInfo.Type.Name, keyValue);
                    IModelViews parent = (IModelViews)base.Model.Parent;
                    IModelListView newModel = (IModelListView)parent[id];
                    if (newModel == null)
                    {
                        newModel = (IModelListView)((ModelNode)base.Model).Clone(id);
                    }
                    base.SetModel(newModel);
                    ApplyModel();
                    itemsDataSource.DisplayableProperties = string.Join(";", RequiredProperties);
                    categoryKey = keyValue;
                }
            }
        }

        private void UpdateCriteria()
        {
            isCriteriaUpdating = true;
            try
            {
                locker.Call("UpdateCriteria");
                if (CategoriesListView != null)
                {
                    BoPhan currentObject = CategoriesListView.CurrentObject as BoPhan;
                    //
                    if (currentObject != null)
                    {
                        List<string> listRoled = Common.Department_GetRoledDepartmentList_ByDepartment(currentObject);
                        //
                        itemsDataSource.Criteria["Filter1"] = new InOperator("BoPhan.Oid", listRoled);
                        itemsDataSource.Criteria["Category"] = new InOperator(criteriaPropertyName, listRoled);
                    }
                    else
                    {
                        itemsDataSource.Criteria["Category"] = new BinaryOperator("Category", string.Empty);
                    }
                }
            }
            finally
            {
                isCriteriaUpdating = false;
            }
        }

        // Properties
        protected CollectionSourceBase CategoriesDataSource
        {
            get
            {
                return categoriesDataSource;
            }
        }

        public ListView CategoriesListView
        {
            get
            {
                return categoriesListView;
            }
        }

        public override object FocusedObject
        {
            get
            {
                return base.FocusedObject;
            }
            set
            {
                ObjectTreeList control = CategoriesListView.Editor.Control as ObjectTreeList;
                if (((control != null) && (value != null)) && !isCriteriaUpdating)
                {
                    control.FocusedObject = ((ICategorizedItem)value).Category;
                }
                base.FocusedObject = value;
            }
        }

        private ITypeInfo ItemClassInfo
        {
            get
            {
                return itemsDataSource.ObjectTypeInfo;
            }
        }

        protected CollectionSourceBase ItemsDataSource
        {
            get
            {
                return itemsDataSource;
            }
        }
    }

    internal class CategorizedListEditorTypeDescriptionProvider : TypeDescriptionProvider
    {
        // Fields
        private CategorizedListEditorCustomTypeDescriptor customTypeDescriptor;
        public Type objectType;

        // Methods
        public CategorizedListEditorTypeDescriptionProvider(Type objectType)
        {
            this.objectType = objectType;
            customTypeDescriptor = new CategorizedListEditorCustomTypeDescriptor();
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            if (this.objectType == objectType)
            {
                return customTypeDescriptor;
            }
            return base.GetTypeDescriptor(objectType, instance);
        }

        public void Setup(PropertyDescriptorCollection additionalProperties)
        {
            PropertyDescriptorCollection descriptors = new PropertyDescriptorCollection(null);
            foreach (PropertyDescriptor descriptor in additionalProperties)
            {
                descriptors.Add(new CategorizedListEditorPropertyDescriptor(descriptor, objectType));
            }
            customTypeDescriptor.AdditionalProperties = descriptors;
        }
    }

    internal class CategorizedListEditorCustomTypeDescriptor : CustomTypeDescriptor
    {
        // Fields
        public PropertyDescriptorCollection AdditionalProperties = new PropertyDescriptorCollection(null);

        // Methods
        public override PropertyDescriptorCollection GetProperties()
        {
            return AdditionalProperties;
        }
    }

    internal class CategorizedListEditorPropertyDescriptor : PropertyDescriptor
    {
        // Fields
        private string bindingMemberName;
        private Type declaringType;
        private PropertyDescriptor descriptor;

        // Methods
        public CategorizedListEditorPropertyDescriptor(PropertyDescriptor descriptor, Type declaringType)
            : base(descriptor)
        {
            this.descriptor = descriptor;
            this.declaringType = declaringType;
        }

        public override bool CanResetValue(object component)
        {
            return descriptor.CanResetValue(component);
        }

        public override object GetValue(object theObject)
        {
            PropertyDescriptor descriptor = null;
            IVariablePropertiesCategorizedItem item = (IVariablePropertiesCategorizedItem)theObject;
            try
            {
                descriptor = item.GetPropertyDescriptorContainer().PropertyDescriptors[descriptor.Name];
            }
            catch
            {
            }
            if (descriptor == null)
            {
                return null;
            }
            return descriptor.GetValue(item.PropertyValueStore);
        }

        public override void ResetValue(object component)
        {
            descriptor.ResetValue(component);
        }

        public override void SetValue(object theObject, object theValue)
        {
            PropertyDescriptor descriptor = null;
            IVariablePropertiesCategorizedItem item = (IVariablePropertiesCategorizedItem)theObject;
            try
            {
                descriptor = item.GetPropertyDescriptorContainer().PropertyDescriptors[descriptor.Name];
            }
            catch
            {
            }
            if (descriptor != null)
            {
                descriptor.SetValue(item.PropertyValueStore, theValue);
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            return descriptor.ShouldSerializeValue(component);
        }

        // Properties
        public string BindingMemberName
        {
            get
            {
                if (string.IsNullOrEmpty(bindingMemberName))
                {
                    ITypeInfo info = XafTypesInfo.Instance.FindTypeInfo(descriptor.ComponentType);
                    bindingMemberName = info.FindMember(Name).BindingName;
                }
                return bindingMemberName;
            }
        }

        public override Type ComponentType
        {
            get
            {
                return declaringType;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return descriptor.IsReadOnly;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return descriptor.PropertyType;
            }
        }
    }
}
