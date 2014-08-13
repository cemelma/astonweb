using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using DAL.Entities;
using DAL.Context;
namespace BLL.LogBL
{
    public class LogtrackManager : IDisposable
    {

        public string User { get; set; }
        public string Data { get; set; }
        public string LogProcess { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Level { get; set; }
        public DateTime LogDate { get; set; }

        public List<Log4Net_Error> GetErrors()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Log4Net_Error.OrderByDescending(d=>d.Date).Take(30).ToList();
                return list;
            };
        }

        public void AddInfoLog(ILog logger)
        {
            object[] args = new object[4];

            if (!string.IsNullOrEmpty(Data))
            {
                args[0] = LogProcess;
                args[1] = User;
                args[2] = Data;
                args[3] = Message;
                logger.InfoFormat("[{0}] [{1}] [{2}] [{3}]", args);
            }
            else
            {
                args[0] = LogProcess;
                args[1] = User;
                args[2] = Message;
                logger.InfoFormat("[{0}] [{1}] [{2}] ", args);
            }
        }

        public void AddFatalLog(ILog logger, Exception errormessage)
        {

            if (!string.IsNullOrEmpty(Data))
            {
                object[] args = new object[4];
                args[0] = LogProcess;
                args[1] = User;
                args[2] = Data;
                args[3] = Message;
                string ermes = string.Format("[{0}] [{1}] [{2}] [{3}]", args);
                logger.Fatal(ermes, errormessage);
            }
            else
            {
                object[] args = new object[4];
                args[0] = LogProcess;
                args[1] = User;
                args[2] = Message;
                string ermes = string.Format("[{0}] [{1}] [{2}] ", args);
                logger.Fatal(ermes, errormessage);

            }



        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
