using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Common.DTOs
{
    public class DTOBase
    {
        public DTOBase()
        {
            SuccessesList = new List<Success>();
            ErrorsList = new List<Error>();
            InfoList = new List<Info>();
            WarningList = new List<Warning>();
        }

        #region Properties

        public bool HasError { get => ErrorsList.Count > 0; }

        public bool HasSuccess { get => SuccessesList.Count > 0; }

        #endregion

        #region Messages

        public List<Success> SuccessesList { get; set; }
        public List<Error> ErrorsList { get; set; }
        public List<Info> InfoList { get; set; }
        public List<Warning> WarningList { get; set; }

        public void AddError(string message)
        {
            ErrorsList.Add(new Error
            {
                Message = message,
            });
        }

        public void AddSuccess(string message)
        {
            SuccessesList.Add(new Success
            {
                Message = message
            });
        }

        public void AddInfo(string message)
        {
            InfoList.Add(new Info
            {
                Message = message
            });
        }

        public void AddWarning(string message)
        {
            WarningList.Add(new Warning
            {
                Message = message
            });
        }

        #endregion
    }

    public class Error
    {
        public string Message { get; set; }
    }

    public class Success
    {
        public string Message { get; set; }
    }

    public class Info
    {
        public string Message { get; set; }
    }

    public class Warning
    {
        public string Message { get; set; }
    }
}
