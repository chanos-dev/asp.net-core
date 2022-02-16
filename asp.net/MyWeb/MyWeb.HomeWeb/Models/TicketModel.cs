using MyWeb.Lib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Models
{
    public class TicketModel
    {        
        public long TicketID { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public static List<TicketModel> GetList(string status)
        {
            using (var conn = new DBHelper())
            {
                var sql = "select ticket_id TicketID, title Title, status Status from t_ticket where status = @status order by ticket_id";
                return conn.Query<TicketModel>(sql, param: new { status }).ToList();
            }
        }

        public int Update()
        {
            string sql = @"
update t_ticket
set title = @title
where ticket_id = @ticket_id
";

            using (var conn = new DBHelper())
            {
                int r = 0;
                try
                {
                    conn.BeginTransaction();
                    r = conn.Execute(sql, param: new { @title = Title, @ticket_id = TicketID });
                    conn.Commit();
                }
                catch(Exception ex)
                {
                    conn.Rollback();
                    // ex logging
                }

                return r;
            }
        }
    }
}
