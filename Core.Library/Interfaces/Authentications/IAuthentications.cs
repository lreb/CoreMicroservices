using System;
using Core.Library.Data.DataModels;
using Core.Library.Messages;
using Core.Library.Settings;

namespace Core.Library.Interfaces.Authentications
{
    public interface IAuthentications
    {
        StandardResult Authenticate(Users user, AppSettings appSettings);
    }
}
