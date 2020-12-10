using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectRequest.Models;
using System.Data;

namespace ProjectRequest
{
    public partial class ExcelExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectRequestEntities request = new ProjectRequestEntities();
            IEnumerable<Request> info = request.Requests.OrderByDescending(i => i.dateRequested).Where(r => r.categoryID != "timeSheet");
            IEnumerable<Models.Attachment> attachmnets = request.Attachments;

            DataTable projectTable = new DataTable();

            DataColumn column;
            DataRow row; 
            DataView view;
            int maxAttachments = Convert.ToInt16(attachmnets.GroupBy(r => r.requestID).OrderByDescending(gp => gp.Count()).Take(1).Select(r => r.Key));

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CT#";
            projectTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CCID";
            projectTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "DateSubmitted";
            projectTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Requester";
            projectTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DueDate");
            column.ColumnName = "DueDate";
            projectTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ProjectName";
            projectTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Location";
            projectTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Category";
            projectTable.Columns.Add(column);

            foreach (var record in info)
            {
                row = projectTable.NewRow();
                row["CT#"] = record.currentTrackID;
                row["CCID"] = record.customID;
                row["DateSubmitted"] = record.dateRequested;
                row["Requester"] = record.name;
                row["DueDate"] = record.dueDate;
                row["ProjectName"] = record.projectName;
                row["Location"] = record.location;
                row["Category"] = record.categoryID;


            }
        }
    }
}