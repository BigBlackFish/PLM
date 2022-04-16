﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLM.Common
{
    public class Singleton<T> where T : class, new()
    {
        private Singleton() { }

        private static readonly object locker = new object();

        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                            _instance = new T();
                    }
                }
                return _instance;
            }
        }
    }
}
