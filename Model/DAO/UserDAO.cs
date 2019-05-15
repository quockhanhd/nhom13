using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.Common;
namespace Model.DAO
{
    public class UserDAO
    {
        OnlineCourse db = new OnlineCourse();
        public int Login(string userName, string passWord, bool AdminLogin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (AdminLogin == true)
                {
                    if (result.GroupID == "1" || result.GroupID == "2")
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }

        public User GetByUserName(string userName)
        {
            return db.Users.Single(x => x.UserName == userName);
        }
       
    }
}
