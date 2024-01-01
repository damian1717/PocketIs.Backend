using PocketIS.Application.Common.Interfaces;
using System;

namespace PocketIS.Repositories
{
    public class BaseRepository
    {
        private readonly IUserProvider _userProvider;
        public BaseRepository(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        protected Guid? UserId
        {
            get { return _userProvider.GetUserId(); }
        }

        protected Guid? CompanyId
        {
            get { return _userProvider.GetCompanyId(); }
        }
    }
}
