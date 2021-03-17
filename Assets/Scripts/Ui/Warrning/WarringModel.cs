using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public delegate void WarringEvent();
    public class WarringModel
    {
        public WarringEvent warringEvent;
        public string valuse;
        public float timer;

        public WarringModel(string _valuse,WarringEvent _event=null,float _timer=-1)
        {
            valuse = _valuse;
            warringEvent = _event;
            timer = _timer;
        }
    }

