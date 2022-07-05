using System;

namespace Common.Model
{
    public interface IUser
    {

    }

    public abstract class TT
    {
        public string TName { get; set; }
    }

    public class User : TT, IUser
    {
        public User()
        {

        }

        public string Name { get; set; } = "sunny";
    }


    public class CUser : User, IUser
    {
        public CUser()
        {

        }
    }
}
