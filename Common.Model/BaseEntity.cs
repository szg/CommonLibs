using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public abstract class Entity
    {
        public bool IsSuccess { get; set; }
        public string ErrMsg { get; set; }
    }

    public abstract class BaseEntity<T> : Entity
    {
        public T Data { get; set; }
    }

    public class TEntity : BaseEntity<User>
    {

    }
}
