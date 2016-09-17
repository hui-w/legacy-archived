using System;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime from = DateTime.MinValue;
        DateTime to = DateTime.MinValue;
        int days = 0;

        #region Get the input
        if (Request.QueryString["F"] != null)
        {
            if (DateTime.TryParse(Request.QueryString["F"], out from))
            {
                Int32.TryParse(Request.QueryString["D"], out days);
                DateTime.TryParse(Request.QueryString["T"], out to);
            }
        }
        #endregion

        #region Date From
        if (from.Equals(DateTime.MinValue))
        {
            this.litDateFrom.Text = "<script type=\"text/javascript\">DateInput('F', true, 'YYYY-MM-DD')</script>";
        }
        else
        {
            string format = "<script type=\"text/javascript\">DateInput('F', true, 'YYYY-MM-DD', '{0}')</script>";
            this.litDateFrom.Text = string.Format(format, this.FormatDateTime(from));
        }
        #endregion

        #region Days
        if (days == 0)
        {
            this.litDays.Text = "<input type=\"text\" name=\"D\" class=\"textbox\" accesskey=\"d\" maxlength=\"6\" />";
        }
        else
        {
            string format = "<input type=\"text\" name=\"D\" value=\"{0}\" class=\"textbox\" accesskey=\"d\" maxlength=\"6\" />";
            this.litDays.Text = string.Format(format, days.ToString());
        }
        #endregion

        #region Date To
        if (to.Equals(DateTime.MinValue))
        {
            this.litDateTo.Text = "<script type=\"text/javascript\">DateInput('T', false, 'YYYY-MM-DD')</script>";
        }
        else
        {
            string format = "<script type=\"text/javascript\">DateInput('T', false, 'YYYY-MM-DD', '{0}')</script>";
            this.litDateTo.Text = string.Format(format, this.FormatDateTime(to));
        }
        #endregion To

        #region Calc and Output
        if (!from.Equals(DateTime.MinValue))
        {
            bool showOutput = false;
            StringBuilder sbOutput = new StringBuilder("<div class=\"output\">");
            if (!days.Equals(0))
            {
                try
                {
                    DateTime dt = from.AddDays((double)days);
                    string format = "<div><span class=\"variable\">{0}</span> day(s) after <span class=\"source\">{1}</span> is <span class=\"result\">{2}</span></div>";
                    sbOutput.AppendFormat(format, days.ToString(), this.FormatDateTime(from), this.FormatDateTime(dt));
                    showOutput = true;
                }
                catch { }
            }

            if (!to.Equals(DateTime.MinValue))
            {
                try
                {
                    TimeSpan ts = to.Subtract(from);
                    string format = "<div><span class=\"variable\">{0}</span> is <span class=\"result\">{1}</span> day(s) before <span class=\"source\">{2}</span></div>";
                    sbOutput.AppendFormat(format, this.FormatDateTime(from), ts.Days.ToString(), this.FormatDateTime(to));
                    showOutput = true;
                }
                catch { }
            }
            sbOutput.AppendLine("</div>");

            this.litOutput.Text = showOutput ? sbOutput.ToString() : string.Empty;
        }
        #endregion

        this.litCopyYear.Text = DateTime.Now.Year.ToString();
    }

    /// <summary>
    /// Format the datetime
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    protected string FormatDateTime(DateTime dt)
    {
        return string.Format("{0:yyyy-MM-dd}", dt);
    }
}
