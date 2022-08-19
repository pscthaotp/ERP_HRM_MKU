using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.NhanSu;
using System.Data.SqlClient;
using ERP.Module.Enum.Systems;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.Commons
{
    public static partial class Common
    {
        /// <summary>
        /// Lấy ngày giờ hệ thống dựa trên time sql của server
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerCurrentTime()
        {
            using (SqlCommand cm = new SqlCommand("Select getdate()", DataProvider.GetConnection()))
            {
                return Convert.ToDateTime(cm.ExecuteScalar());
            }
        }

        /// <summary>
        /// Set Time for DateTime value (use in Tax calculator)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="style">0: start, 1: end, 2: start month, 3: end month, 4: start year, 5: end year, 6: start week, 7: end week, 8: end work week</param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime source, SetTimeEnum type)
        {
            int hh, mm, ss;
            if (type == SetTimeEnum.StartDay)
            {
                hh = source.Hour;
                mm = source.Minute;
                ss = source.Second;

                source = source.AddHours(-hh);
                source = source.AddMinutes(-mm);
                source = source.AddSeconds(-ss);
            }
            else if (type == SetTimeEnum.EndDay)
            {
                hh = 23 - source.Hour;
                mm = 59 - source.Minute;
                ss = 59 - source.Second;

                source = source.AddHours(hh);
                source = source.AddMinutes(mm);
                source = source.AddSeconds(ss);
            }
            else if (type == SetTimeEnum.StartMonth)
            {
                source = new DateTime(source.Year, source.Month, 1);
                source = SetTime(source, SetTimeEnum.StartDay);
            }
            else if (type == SetTimeEnum.EndMonth)
            {
                source = new DateTime(source.Year, source.Month, 1).AddMonths(1).AddDays(-1);
                source = SetTime(source, SetTimeEnum.EndDay);
            }
            else if (type == SetTimeEnum.StartYear)
            {
                source = new DateTime(source.Year, 1, 1);
                source = SetTime(source, SetTimeEnum.StartDay);
            }
            else if (type == SetTimeEnum.EndYear)
            {
                source = new DateTime(source.Year, 12, 31);
                source = SetTime(source, SetTimeEnum.EndDay);
            }
            else if (type == SetTimeEnum.StartWeek)
            {
                DayOfWeek dayOfWeek = source.DayOfWeek;
                if (dayOfWeek != DayOfWeek.Sunday)
                    source = source.AddDays(-((int)dayOfWeek - 1));
                else
                    source = source.AddDays(-7);
                source = SetTime(source, SetTimeEnum.EndDay);
            }
            else if (type == SetTimeEnum.EndWeek)
            {
                DayOfWeek dayOfWeek = source.DayOfWeek;
                if (dayOfWeek != DayOfWeek.Sunday)
                    source = source.AddDays((7 - (int)dayOfWeek));
                source = SetTime(source, SetTimeEnum.EndDay);
            }
            else if (type == SetTimeEnum.EndWorkWeek)
            {
                DayOfWeek dayOfWeek = source.DayOfWeek;
                if (dayOfWeek != DayOfWeek.Sunday)
                    source = source.AddDays((5 - (int)dayOfWeek));
                source = SetTime(source, SetTimeEnum.EndDay);
            }

            return source;
        }

        /// <summary>
        /// là T7, CN và các ngày lễ
        /// </summary>
        /// <param name="source"></param>
        /// <param name="DenNgay"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool IsHoliday(this DateTime source, Session session)
        {
            if (source.DayOfWeek == DayOfWeek.Saturday
                || source.DayOfWeek == DayOfWeek.Sunday)
                return true;

            XPCollection<CC_NgayNghiTrongNam> list = new XPCollection<CC_NgayNghiTrongNam>(session, CriteriaOperator.Parse("NgayNghi.Year=?", source.Year));
            foreach (var item in list)
            {
                if (source.Date == item.NgayNghi.Date)
                    return true;
            }
            return false;
        }
     

        /// <summary>
        /// Tính số ngày trong khoảng từ ngày, đến ngày, không tính T7, CN
        /// </summary>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int GetDayNumberSubtrackWeekend_DaiHoc(this DateTime TuNgay, DateTime DenNgay, Session session)
        {
            int SoNgay = 0;
            if (DenNgay.CompareTo(DateTime.MinValue) != 0 & TuNgay.CompareTo(DateTime.MinValue) != 0)
            {
                //
                for (DateTime ngay = TuNgay; ngay <= DenNgay; ngay = ngay.AddDays(1))
                {
                    if (ngay.DayOfWeek != DayOfWeek.Saturday & ngay.DayOfWeek != DayOfWeek.Sunday)
                    {
                        SoNgay++;
                    }
                }
            }
            return SoNgay;
        }


        /// <summary>
        /// Tính số ngày trong khoảng từ ngày, đến ngày, không tính CN
        /// </summary>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static decimal GetDayNumberSubtrackWeekend_ManNonAndPhoThong(this DateTime TuNgay, DateTime DenNgay, Session session)
        {
            decimal SoNgay = 0;
            if (DenNgay.CompareTo(DateTime.MinValue) != 0 & TuNgay.CompareTo(DateTime.MinValue) != 0)
            {
                //
                for (DateTime ngay = TuNgay; ngay <= DenNgay; ngay = ngay.AddDays(1))
                {
                    if (ngay.DayOfWeek != DayOfWeek.Sunday)
                    {
                        SoNgay++;
                    }
                }
            }
            //
            return SoNgay;
        }

        /// <summary>
        /// Tính số ngày trong khoảng từ ngày, đến ngày, tính cả T7, CN và các ngày lễ
        /// </summary>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int GetDayNumberAddHoliday(this DateTime TuNgay, DateTime DenNgay)
        {
            int SoNgay = 0;
            if (DenNgay.CompareTo(DateTime.MinValue) != 0 & TuNgay.CompareTo(DateTime.MinValue) != 0)
            {
                SoNgay = DenNgay.Subtract(TuNgay).Days;
            }
            return SoNgay;
        }

        /// <summary>
        /// Tính số ngày trong khoảng từ ngày, đến ngày
        /// </summary>
        /// <param name="TuNgay"></param>
        /// <param name="DenNgay"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static decimal SoNgayTrongThangTheoLoaiGioLamViec(this DateTime TuNgay, DateTime DenNgay, Session session)
        {
            decimal SoNgay = 0;
            if (CauHinhChung_GetCauHinhChung != null && CauHinhChung_GetCauHinhChung.CauHinhHoSo != null)
            {
                LoaiGioLamViec loaiGioLamViec = CauHinhChung_GetCauHinhChung.CauHinhHoSo.LoaiGioLamViec;
                if (loaiGioLamViec != null)
                {
                    if ((loaiGioLamViec.CaChamCong_CN != null && loaiGioLamViec.CaChamCong_CN.TongSoGioLamViecCaNgay >= 8) || (loaiGioLamViec.CaChamCong_T7 != null  && loaiGioLamViec.CaChamCong_T7.TongSoGioLamViecCaNgay >= 8))
                        SoNgay = 24;
                    else if (loaiGioLamViec.CaChamCong_CN == null && loaiGioLamViec.CaChamCong_T7 == null)
                        SoNgay = 20;
                    else
                        SoNgay = 22;
                }
            }
            //
            return SoNgay;
        }

        /// <summary>
        /// Tính số tháng
        /// </summary>
        /// <param name="batDau">thang bat dau</param>
        /// <param name="ketThuc">thang ket thuc</param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int GetMonthNumber(this DateTime batDau, DateTime ketThuc)
        {
            int count = 0;

            if (batDau.CompareTo(DateTime.MinValue) != 0 && ketThuc.CompareTo(DateTime.MinValue) != 0)
            {
                count = 12 * (ketThuc.Year - batDau.Year) + (ketThuc.Month - batDau.Month) + 1;
            }

            return Math.Abs(count);
        }

        /// <summary>
        /// Tính số năm
        /// </summary>
        /// <param name="batDau">thang bat dau</param>
        /// <param name="ketThuc">thang ket thuc</param>
        /// <returns></returns>
        public static int GetYearNumber(this DateTime batDau, DateTime ketThuc)
        {
            int count = 0;

            if (batDau.CompareTo(DateTime.MinValue) != 0 && ketThuc.CompareTo(DateTime.MinValue) != 0)
            {
                count = Math.Abs(ketThuc.Year - batDau.Year);
            }

            return count;
        }
    }
}
