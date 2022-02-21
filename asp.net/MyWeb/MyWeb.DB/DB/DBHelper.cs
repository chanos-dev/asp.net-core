using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MyWeb.Lib.DB
{
    public class DBHelper : IDisposable
    {
        private bool disposedValue;

        private MySqlConnection SqlConnection { get; set; }
        private MySqlTransaction Trans { get; set; }

        public DBHelper()
        {
            SqlConnection = new MySqlConnection(@"Server=192.168.0.105;Port=8448;Database=myweb;Uid=chanos-dev;Pwd=!Qweasdzxc123;");
        }

        public void BeginTransaction()
        {
            if (SqlConnection.State != ConnectionState.Open)
                SqlConnection.Open();

            Trans = SqlConnection.BeginTransaction();
        }
        
        public void Commit()
        {
            Trans.Commit();
            Trans.Dispose();
            Trans = null;
        }

        public void Rollback()
        {
            Trans.Rollback();
            Trans.Dispose();
            Trans = null;
        }

        public List<T> Query<T>(string sql, object param) => SqlConnection.Query<T>(sql, param, Trans).ToList();

        public T QuerySingleOrDefault<T>(string sql, object param) => SqlConnection.QuerySingleOrDefault<T>(sql, param, Trans);

        public int Execute(string sql, object param) => SqlConnection.Execute(sql, param, Trans);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리형 상태(관리형 개체)를 삭제합니다.
                }

                // TODO: 비관리형 리소스(비관리형 개체)를 해제하고 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                disposedValue = true;

                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans.Dispose();
                }

                SqlConnection.Dispose();                
            }
        }

        // // TODO: 비관리형 리소스를 해제하는 코드가 'Dispose(bool disposing)'에 포함된 경우에만 종료자를 재정의합니다.
        ~DBHelper()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
