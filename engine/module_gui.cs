using System;
using System.Web;
using System.Web.UI.WebControls;
using xFarmFrenzy.Isui;

namespace xPolimer
{
    public class TGUI
    {
        public const String _GUISMENAONETEXT = " 1-я смена ";
        public const String _GUISMENATWOTEXT = " 2-я смена ";
        public const int _GUISMENAONE = 20;
        public const int _GUISMENATWO = 08;
        public string[] MonthList = {
              "Январь",
              "Февраль",
              "Март",
              "Апрель",
              "Май",
              "Июнь",
              "Июль",
              "Август",
              "Сентябрь",
              "Октябрь",
              "Ноябрь",
              "Декабрь"
            };

        public String GetUserName()
        {
            return HttpContext.Current.User.Identity.Name.Replace("PAZ\\", "").Replace("paz\\", "").Replace("AOK\\", "").Replace("aok\\", "");
        }

        public int GetUserRightForResource(int IdRes)
        {
            Isuiservice Service = new Isuiservice();
            String Login = GetUserName();
            TUserInfo UserInfo = Service.getUserInfoByLogin(Login);
            int UserRight = Service.getRight(UserInfo.Id, IdRes);

            return UserRight;
        }

        public String ExtendNumber(int Number)
        {
            if (Number < 10)
                return "0" + Number.ToString();
            else
                return Number.ToString();
        }

        public void FillYear(DropDownList Box)
        {
            int Index;
            int Year = DateTime.Today.Year;
            Box.Items.Clear();

            for (Index = Year; Index >= Year - 10; Index--)
            {
                ListItem Item = new ListItem();
                Item.Value = Index.ToString();
                Box.Items.Add(Item);
            }
        }

        public void FillMonth(DropDownList Box)
        {
            int Index;

            Box.Items.Clear();
            for (Index = 0; Index < 12; Index++)
            {
                ListItem Item = new ListItem();
                Item.Text = MonthList[Index];
                Item.Value = (Index + 1).ToString();
                Box.Items.Add(Item);
            }
            Box.SelectedIndex = DateTime.Today.Month - 1;
        }

        public void FillMonth(DropDownList Box, int MonthCount)
        {
            int Index;
            int CurrentMonth = DateTime.Today.Month;

            Box.Items.Clear();
            for (Index = --CurrentMonth; Index < CurrentMonth + MonthCount; Index++)
            {
                ListItem Item = new ListItem();
                Item.Text = MonthList[Index];
                Item.Value = (Index + 1).ToString();
                Box.Items.Add(Item);
            }
            Box.SelectedIndex = 0;
        }

        public void FillMonthDated(DropDownList Box)
        {
            int MonthIndex = DateTime.Today.Month - 1;
            int YearIndex = DateTime.Today.Year;
            Box.Items.Clear();

            ListItem Item = new ListItem();
            Item.Text = MonthList[MonthIndex];
            Item.Value = new DateTime(YearIndex, DateTime.Today.Month, 1, 08, 00, 00).ToString();
            Box.Items.Add(Item);

            // Переход через год
            if (MonthIndex + 1 > 11)
            {
                MonthIndex = -1;
                YearIndex++;
            }

            Item = new ListItem();
            Item.Text = MonthList[MonthIndex + 1];
            Item.Value = new DateTime(YearIndex, DateTime.Today.AddMonths(1).Month, 1, 08, 00, 00).ToString();
            Box.Items.Add(Item);
        }

        public void FillDaySmena(DropDownList Box, int DayBefore, int DayAfter)
        {
            DateTime dateNow = DateTime.Now;
            dateNow = TORA.聞く(dateNow);
            DateTime dateBegin = dateNow.AddDays(DayBefore);
            DateTime dateEnd = dateNow.AddDays(DayAfter);
            String DateText;

            Box.Items.Clear();
            while (dateBegin <= dateEnd)
            {
                // 25 20:00 = 26 1смена
                // 26 08:00 = 26 2смена
                // 26 20:00 = 27 1смена
                // 27 08:00 = 27 2смена
                ListItem ItemOne = new ListItem();
                ItemOne.Value = dateBegin.ToString();

                dateBegin = dateBegin.AddHours(12);
                DateText = ExtendNumber(dateBegin.Day) + "." + ExtendNumber(dateBegin.Month);
                if (dateBegin.Hour == _GUISMENATWO)
                    ItemOne.Text = DateText + _GUISMENAONETEXT;
                else
                    ItemOne.Text = DateText + _GUISMENATWOTEXT;
                Box.Items.Add(ItemOne);
                if (dateBegin == dateNow.AddHours(12)) Box.SelectedIndex = Box.Items.Count - 1;


                if (!(dateBegin <= dateEnd)) continue;

                ListItem ItemTwo = new ListItem();
                ItemTwo.Value = dateBegin.ToString();

                dateBegin = dateBegin.AddHours(12);
                DateText = ExtendNumber(dateBegin.Day) + "." + ExtendNumber(dateBegin.Month);
                if (dateBegin.Hour == _GUISMENATWO)
                    ItemTwo.Text = DateText + _GUISMENAONETEXT;
                else
                    ItemTwo.Text = DateText + _GUISMENATWOTEXT;
                Box.Items.Add(ItemTwo);
                if (dateBegin == dateNow.AddHours(12)) Box.SelectedIndex = Box.Items.Count - 1;
            }
        }
    }
}
