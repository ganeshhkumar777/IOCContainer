using System;
using System.Collections.Generic;
using System.Linq;

namespace IOCContainer
{
    public class IOCContainerBuilder
    {

        private IOCContainer containers;
        public IOCContainerBuilder  Add<t,u>() where u : t{
            if(containers ==null){
                containers=new IOCContainer();
            }

            containers.dic.Add(typeof(t),typeof(u));
            return this;
        }

        public Type Get(Type t) {
            if(containers.dic.ContainsKey(t))
            return containers.dic[t];
            throw new InvalidOperationException("type"+t+"is not registered in IOC container");
        }

        private class IOCContainer{
           public Dictionary<Type,Type> dic;
           public IOCContainer(){
                dic=new Dictionary<Type,Type>();
            }
        }
    }
}
