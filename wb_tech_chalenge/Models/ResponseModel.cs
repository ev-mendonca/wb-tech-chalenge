using Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wb_tech_chalenge.Models
{
    [Serializable]
    public class ResponseModel
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public Conta Conta { get; set; }
    }


    //public class ResponseModel<T>: ResponseModel 
    //{
    //    public T ResponseObj { get; set; }
    //}

}
