using MyWeb.Lib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Models
{
    public class BoardModel
    {
        public uint Idx { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public uint RegUser { get; set; }
        public string RegUsername { get; set; }
        public DateTime RegDate { get; set; }
        public uint ViewCnt { get; set; }
        public short StatusFlag { get; set; }

        public static List<BoardModel> GetList(string search)
        {
            using (var db = new DBHelper())
            {
                string sql = @"
SELECT
	idx
	,title
    ,contents
    ,reg_user RegUser
    ,reg_username RegUserName
    ,reg_date RegDate
    ,view_cnt ViewCnt
    ,status_flag StatusFlag
FROM
	t_board
WHERE
	title LIKE CONCAT('%', IFNULL(@search, ''), '%')
ORDER BY
    idx DESC
";
                return db.Query<BoardModel>(sql, new { search = search });
            }
        }

        public static BoardModel Get(uint idx)
        {
            using (var db = new DBHelper())
            {
                string sql = @"
SELECT
	idx
	,title
    ,contents
    ,reg_user RegUser
    ,reg_username RegUserName
    ,reg_date RegDate
    ,view_cnt ViewCnt
    ,status_flag StatusFlag
FROM
	t_board 
WHERE
	idx = @idx
";
                return db.QuerySingleOrDefault<BoardModel>(sql, new { idx = idx });
            }
        }

        private void CheckContents()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
            {
                throw new Exception("제목이 빈칸입니다");
            }
            if (string.IsNullOrWhiteSpace(this.Contents))
            {
                throw new Exception("내용이 빈칸입니다");
            }

            if (string.IsNullOrWhiteSpace(this.RegUsername))
            {
                throw new Exception("작성자가 없습니다");
            }
        }

        public int Insert()
        {
            CheckContents();

            string sql = @"
INSERT INTO t_board (
	title
	,contents
    ,reg_user
    ,reg_username
    ,reg_date
    ,view_Cnt
    ,status_flag
)
VALUES (
    @title
	,@contents
    ,@reg_user
    ,@reg_username
    ,now()
    ,0
    ,0
)
";
            using (var db = new DBHelper())
            {
                return db.Execute(sql, new { @title = Title, @contents = Contents, @reg_user = RegUser, @reg_username = RegUsername });
            }
        }

        public int Update()
        {
            CheckContents();

            string sql = @"
UPDATE t_board
SET
    title = @title
	,contents = @contents
WHERE
    idx = @idx
";
            using (var db = new DBHelper())
            {
                return db.Execute(sql, new { @title = Title, @contents = Contents, @idx = Idx});
            }
        }

        public int Delete()
        {
            string sql = @"
DELETE FROM t_board
WHERE
    idx = @idx
";
            using (var db = new DBHelper())
            {
                return db.Execute(sql, new { @idx = Idx });
            }
        }
    }
}
