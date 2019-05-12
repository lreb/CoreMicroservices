using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Library.Messages
{
    /// <summary>
    /// Standard result.
    /// </summary>
    public class StandardResult
    {
        public int StatusCode;
        public Result ResultCode;
        public string EventReference;
        public bool Success;
        public bool Warning;
        public bool Fail;
        public object Data;
        public string Message;
        public ICollection<MessageDetail> MessageDetail;

        public  StandardResult()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Core.Library.Messages.StandardResult"/> class.
        /// </summary>
        /// <param name="resultCode">Result code.</param>
        /// <param name="message">Message.</param>
        /// <param name="statusCode">Status code.</param>
        private StandardResult(Result resultCode, string message, int statusCode = 200)
        {
            ResultCode = resultCode;
            Message = message;
            SetResultStatus(statusCode);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Core.Library.Messages.StandardResult"/> class.
        /// </summary>
        /// <param name="resultCode">Result code.</param>
        /// <param name="message">Message.</param>
        /// <param name="data">Data.</param>
        /// <param name="statusCode">Status code.</param>
        private StandardResult(Result resultCode, string message, object data, int statusCode = 200)
        {
            ResultCode = resultCode;
            Message = message;
            Data = data;
            SetResultStatus(statusCode);
        }
        /// <summary>
        /// Sets the result status.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        private void SetResultStatus(int statusCode)
        {
            switch (ResultCode)
            {
                case Result.ERROR:
                    Fail = true;
                    Success = false;
                    Warning = false;
                    StatusCode = statusCode;
                    break;
                case Result.SUCCESS:
                    Fail = false;
                    Success = true;
                    Warning = false;
                    StatusCode = statusCode;
                    break;
                case Result.WARNING:
                    Fail = false;
                    Success = false;
                    Warning = true;
                    StatusCode = statusCode;
                    break;
            }
        }
        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <returns>The result.</returns>
        /// <param name="message">Message.</param>
        public static StandardResult SuccessResult(string message)
        {
            return new StandardResult(Result.SUCCESS, message);
        }
        /// <summary>
        /// Successes the result.
        /// </summary>
        /// <returns>The result.</returns>
        /// <param name="message">Message.</param>
        /// <param name="data">Data.</param>
        public static StandardResult SuccessResult(string message, object data)
        {
            return new StandardResult(Result.SUCCESS, message, data);
        }
        /// <summary>
        /// Fails the result.
        /// </summary>
        /// <returns>The result.</returns>
        /// <param name="message">Message.</param>
        public static StandardResult FailResult(string message)
        {
            return new StandardResult(Result.ERROR, message);
        }
        /// <summary>
        /// Fails the result.
        /// </summary>
        /// <returns>The result.</returns>
        /// <param name="message">Message.</param>
        /// <param name="data">Data.</param>
        public static StandardResult FailResult(string message, object data)
        {
            return new StandardResult(Result.ERROR, message, data);
        }
        /// <summary>
        /// Warnings the result.
        /// </summary>
        /// <returns>The result.</returns>
        /// <param name="message">Message.</param>
        public static StandardResult WarningResult(string message)
        {
            return new StandardResult(Result.WARNING, message);
        }
        /// <summary>
        /// Warnings the result.
        /// </summary>
        /// <returns>The result.</returns>
        /// <param name="message">Message.</param>
        /// <param name="data">Data.</param>
        public static StandardResult WarningResult(string message, object data)
        {
            return new StandardResult(Result.WARNING, message, data);
        }
        /// <summary>
        /// Adds the message detail.
        /// </summary>
        /// <param name="messageDetail">Message detail.</param>
        public void AddMessageDetail(MessageDetail messageDetail)
        {
            if (MessageDetail == null)
            {
                MessageDetail = new List<MessageDetail>();
            }
            MessageDetail.Add(messageDetail);
        }
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Core.Library.Messages.StandardResult"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Core.Library.Messages.StandardResult"/>.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    /// <summary>
    /// Global message type.
    /// </summary>
    public enum GlobalMessageType
    {
        Success,
        Warning,
        Fail
    }
    /// <summary>
    /// Result.
    /// </summary>
    public enum Result { ERROR, SUCCESS, WARNING }
    /// <summary>
    /// Message detail.
    /// </summary>
    public class MessageDetail
    {
        public Result Type;
        public string Key;
        public string Message;
        public string Detail;
        public MessageDetail()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Core.Library.Messages.MessageDetail"/> class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="message">Message.</param>
        /// <param name="messageDetail">Message detail.</param>
        public MessageDetail(string key, string message, string messageDetail)
        {
            Key = key;
            Message = message;
            Detail = messageDetail;
        }
        /// <summary>
        /// Creates the success message.
        /// </summary>
        /// <returns>The success message.</returns>
        /// <param name="key">Key.</param>
        /// <param name="message">Message.</param>
        /// <param name="messageDetail">Message detail.</param>
        public static MessageDetail CreateSuccessMessage(string key, string message, string messageDetail)
        {
            MessageDetail m = new MessageDetail(key, message, messageDetail);
            m.SetType(Result.SUCCESS);
            return m;
        }
        /// <summary>
        /// Creates the error message.
        /// </summary>
        /// <returns>The error message.</returns>
        /// <param name="key">Key.</param>
        /// <param name="message">Message.</param>
        /// <param name="messageDetail">Message detail.</param>
        public static MessageDetail CreateErrorMessage(string key, string message, string messageDetail)
        {
            MessageDetail m = new MessageDetail(key, message, messageDetail);
            m.SetType(Result.ERROR);
            return m;
        }
        /// <summary>
        /// Creates the warning message.
        /// </summary>
        /// <returns>The warning message.</returns>
        /// <param name="key">Key.</param>
        /// <param name="message">Message.</param>
        /// <param name="messageDetail">Message detail.</param>
        public static MessageDetail CreateWarningMessage(string key, string message, string messageDetail)
        {
            MessageDetail m = new MessageDetail(key, message, messageDetail);
            m.SetType(Result.WARNING);
            return m;
        }

        public string GetKey()
        {
            return Key;
        }

        public string GetMessage()
        {
            return Message;
        }

        public string GetMessageDetail()
        {
            return Detail;
        }

        public Result GetResultType()
        {
            return Type;
        }

        public void SetKey(string key)
        {
            Key = key;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }

        public void SetMessageDetail(string messageDetail)
        {
            Detail = messageDetail;
        }

        public void SetType(Result type)
        {
            Type = type;
        }

        public bool IsSuccess()
        {
            return Type == Result.SUCCESS;
        }
        /// <summary>
        /// Ises the error.
        /// </summary>
        /// <returns><c>true</c>, if error was ised, <c>false</c> otherwise.</returns>
        public bool IsError()
        {
            return Type == Result.ERROR;
        }
        /// <summary>
        /// Ises the warning.
        /// </summary>
        /// <returns><c>true</c>, if warning was ised, <c>false</c> otherwise.</returns>
        public bool IsWarning()
        {
            return Type == Result.WARNING;
        }
        /// <summary>
        /// Tos the html.
        /// </summary>
        /// <returns>The html.</returns>
        public String ToHtml()
        {
            String typeStr = "";
            switch (Type)
            {
                case Result.SUCCESS:
                    typeStr = "SUCCESS";
                    break;
                case Result.ERROR:
                    typeStr = "ERROR";
                    break;
                case Result.WARNING:
                    typeStr = "WARNING";
                    break;
                default:
                    typeStr = "";
                    break;
            }
            return $"<tr><td>{typeStr}</td><td>{Key}</td><td>{Message}</td><td>{Detail}</td>";
        }
    }
}
