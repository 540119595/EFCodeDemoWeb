using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CodeGenerator
{
    public static class CodeGenerateOption
    {
        public static string DomainNamespace { get; set; }
        public static string IRepositoriesNamespace { get; set; }
        public static string RepositoriesNamespace { get; set; }
        public static string IServicsNamespace { get; set; }
        public static string ServicesNamespace { get; set; }
        public static string ModelsNamespace { get; set; }

        static CodeGenerateOption()
        {
            DomainNamespace = "Domain";
            IRepositoriesNamespace = "Domain.IRepositorys";
            RepositoriesNamespace = "Domain.RepositorysImpl";
            IServicsNamespace = "Domain.IServices";
            ServicesNamespace = "Domain.ServicesImpl";
        }
    }
}
