﻿using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;

namespace ERP.Module.Extends
{
    public static class XafApplicationHelper
    {
        /// <summary>
        /// show view
        /// </summary>
        /// <typeparam name="T">Base object type</typeparam>
        /// <param name="app">xaf application</param>
        /// <param name="obs">IObjectSpace</param>
        /// <param name="obj">T object</param>
        public static void ShowView<T>(this XafApplication app, IObjectSpace obs, T obj) where T : BaseObject
        {
            ShowViewParameters showView = new ShowViewParameters();
            showView.CreatedView = app.CreateDetailView(obs, obj);
            showView.TargetWindow = TargetWindow.NewWindow;
            showView.Context = TemplateContext.View;
            showView.CreateAllControllers = true;

            app.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
        }

        /// <summary>
        /// show popup
        /// </summary>
        /// <typeparam name="T">Base object type</typeparam>
        /// <param name="app">xaf application</param>
        /// <param name="obs">IObjectSpace</param>
        /// <param name="obj">T object</param>
        public static void ShowModelView<T>(this XafApplication app, IObjectSpace obs, T obj) where T : BaseObject
        {
            ShowViewParameters showView = new ShowViewParameters();
            showView.CreatedView = app.CreateDetailView(obs, obj);
            showView.TargetWindow = TargetWindow.NewModalWindow;
            showView.Context = TemplateContext.View;
            showView.CreateAllControllers = true;

            app.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
        }
        /// <summary>
        /// show popup
        /// </summary>
        /// <typeparam name="T">Base object type</typeparam>
        /// <param name="app">xaf application</param>
        /// <param name="obs">IObjectSpace</param>
        /// <param name="obj">T object</param>
        public static void ShowEditModelView<T>(this XafApplication app, IObjectSpace obs, T obj) where T : BaseObject
        {
            ShowViewParameters showView = new ShowViewParameters();
            DetailView view = app.CreateDetailView(obs, obj);
            view.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            showView.CreatedView = view;
            showView.TargetWindow = TargetWindow.NewModalWindow;
            showView.Context = TemplateContext.View;
            showView.CreateAllControllers = true;

            app.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
        }
        /// <summary>
        /// Show list view
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <param name="obs"></param>
        /// <param name="list"></param>
        /// <param name="showToolbar"></param>
        public static void ShowListView<T>(this XafApplication app, CollectionSource list, bool showToolbar) where T : BaseObject
        {
            ShowViewParameters showView = new ShowViewParameters();
            showView.CreatedView = app.CreateListView(app.GetListViewId(typeof(T)), list, false);
            showView.TargetWindow = TargetWindow.NewModalWindow;
            showView.Context = TemplateContext.View;
            showView.CreateAllControllers = showToolbar;

            app.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
        }

        /// <summary>
        /// Show detail view
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <param name="obs"></param>
        /// <param name="list"></param>
        /// <param name="showToolbar"></param>
        public static void ShowDetailView(this XafApplication app, IObjectSpace obs, Guid guid, string keyObject)
        {
            Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == keyObject);
            if (objecttype != null)
            {
                object temp = obs.GetObjectByKey(objecttype, guid);
                ShowViewParameters showView = new ShowViewParameters();
                showView.CreatedView = app.CreateDetailView(obs, temp, true);
                showView.TargetWindow = TargetWindow.NewWindow;
                showView.Context = TemplateContext.View;
                showView.CreateAllControllers = true;
                app.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
            }
        }

        /// <summary>
        /// Show list view
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <param name="obs"></param>
        /// <param name="list"></param>
        /// <param name="showToolbar"></param>
        public static void ShowDetailView(this XafApplication app, IObjectSpace obs, string keyObject)
        {
            Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == keyObject);
            if (objecttype == null)
                return;
            ShowViewParameters showView = new ShowViewParameters();
            showView.CreatedView = app.CreateDetailView(objecttype, true);
            showView.TargetWindow = TargetWindow.NewWindow;
            showView.Context = TemplateContext.View;
            showView.CreateAllControllers = true;            
            app.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
        }

        public static void CascadeCommit(IObjectSpace os, bool refresh)
        {
            os.CommitChanges();
            if (os is INestedObjectSpace)
            {
                CascadeCommit(((INestedObjectSpace)os).ParentObjectSpace, refresh);
            }
            if (refresh)
            {
                os.Refresh();
            }
        }
    }
}
