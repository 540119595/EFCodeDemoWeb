using Common.BaseDomain;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common.CodeGenerator
{
    /// <summary>
    /// 代码文件生成
    /// <remarks>
    /// 根据实体生成Repositories和Services层的基础代码文件。
    /// </remarks>
    /// </summary>
    public class CodeGenerator
    {
        //private static string DOMAIN_PATH = @"F:\EFCodeDemoWeb\Domain\bin\Debug\netcoreapp2.0\Domain.dll";

        public CodeGenerator()
        {
        }

        public static void Generate(bool ifExsitedCovered = false)
        {
            //byte[] buffer = System.IO.File.ReadAllBytes(DOMAIN_PATH);
            Assembly assembly = Assembly.Load(new AssemblyName(CodeGenerateOption.DomainNamespace));//Assembly.Load(buffer);
            var modelsTypes = assembly.GetTypes()
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
                .Where(type => type.GetTypeInfo().IsClass)
                .Where(type => type.GetTypeInfo().BaseType != null)
                .Where(type => type.GetTypeInfo().BaseType.Equals(typeof(BaseEntity)));

            foreach (var modelsType in modelsTypes)
            {
                GenerateSingle(modelsType, ifExsitedCovered);
            }
        }

        private static void GenerateSingle(Type modelType, bool ifExsitedCovered = false)
        {
            //var modelsNamespace = modelType.Namespace;
            CodeGenerateOption.ModelsNamespace = modelType.Namespace;
            var areasName = modelType.Namespace.Split('.')[modelType.Namespace.Split('.').Length - 1];

            var modelTypeName = modelType.Name;
            var keyTypeName = modelType.GetProperty("Id")?.PropertyType.Name;// String
            GenerateIRepository(modelTypeName, keyTypeName, areasName, ifExsitedCovered);
            GenerateRepository(modelTypeName, keyTypeName, areasName, ifExsitedCovered);
            GenerateIService(modelTypeName, keyTypeName, areasName, ifExsitedCovered);
            GenerateService(modelTypeName, keyTypeName, areasName, ifExsitedCovered);
        }

        /// <summary>
        /// 生成IRepository层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="areasName"></param>
        /// <param name="ifExsitedCovered"></param>
        private static void GenerateIRepository(string modelTypeName, string keyTypeName, string areasName, bool ifExsitedCovered = false)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Substring(0, path.IndexOf("\\bin"));    // F:\EFCodeDemoWeb\XUnitTestCommon
            var parentPath = path.Substring(0, path.LastIndexOf("\\")); // F:\EFCodeDemoWeb
            var iRepositoryPath = CodeGenerateOption.IRepositoriesNamespace.Replace('.', '\\') + "\\" + areasName;
            iRepositoryPath = parentPath + "\\" + iRepositoryPath;    // F:\EFCodeDemoWeb\Domain\IRepositorys\Sys
            if (!Directory.Exists(iRepositoryPath))
            {
                //iRepositoryPath = parentPath + "\\IRepositories";
                Directory.CreateDirectory(iRepositoryPath);
            }
            var fullPath = iRepositoryPath + "\\I" + modelTypeName + "Repository.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("IRepositoryTemplate.txt");
            content = content.Replace("{ModelsNamespace}", CodeGenerateOption.ModelsNamespace)
                .Replace("{IRepositoriesNamespace}", CodeGenerateOption.IRepositoriesNamespace + "." + areasName)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成Repository层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered"></param>
        private static void GenerateRepository(string modelTypeName, string keyTypeName, string areasName, bool ifExsitedCovered = false)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var RepositoryPath = CodeGenerateOption.RepositoriesNamespace.Replace('.', '\\') + "\\" + areasName;
            RepositoryPath = parentPath + "\\" + RepositoryPath;
            if (!Directory.Exists(RepositoryPath))
            {
                Directory.CreateDirectory(RepositoryPath);
            }
            var fullPath = RepositoryPath + "\\" + modelTypeName + "Repository.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("RepositoryTemplate.txt");
            content = content.Replace("{ModelsNamespace}", CodeGenerateOption.ModelsNamespace)
                .Replace("{IRepositoriesNamespace}", CodeGenerateOption.IRepositoriesNamespace)
                .Replace("{RepositoriesNamespace}", CodeGenerateOption.RepositoriesNamespace)
                .Replace("{AreasName}", areasName)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        private static void GenerateIService(string modelTypeName, string keyTypeName, string areasName, bool ifExsitedCovered = false)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var iServicesPath = CodeGenerateOption.IServicsNamespace.Replace('.', '\\') + "\\" + areasName;
            iServicesPath = parentPath + "\\" + iServicesPath;
            if (!Directory.Exists(iServicesPath))
            {
                Directory.CreateDirectory(iServicesPath);
            }
            var fullPath = iServicesPath + "\\I" + modelTypeName + "Service.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("IServiceTemplate.txt");
            content = content.Replace("{ModelsNamespace}", CodeGenerateOption.ModelsNamespace)
                .Replace("{IServicsNamespace}", CodeGenerateOption.IServicsNamespace + "." + areasName)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        private static void GenerateService(string modelTypeName, string keyTypeName, string areasName, bool ifExsitedCovered = false)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Substring(0, path.IndexOf("\\bin"));
            var parentPath = path.Substring(0, path.LastIndexOf("\\"));
            var servicesPath = CodeGenerateOption.ServicesNamespace.Replace('.', '\\') + "\\" + areasName;
            servicesPath = parentPath + "\\" + servicesPath;
            if (!Directory.Exists(servicesPath))
            {
                Directory.CreateDirectory(servicesPath);
            }
            var fullPath = servicesPath + "\\" + modelTypeName + "Service.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("ServiceTemplate.txt");
            content = content.Replace("{ModelsNamespace}", CodeGenerateOption.ModelsNamespace)
                .Replace("{IRepositoriesNamespace}", CodeGenerateOption.IRepositoriesNamespace)
                .Replace("{IServicsNamespace}", CodeGenerateOption.IServicsNamespace)
                .Replace("{ServicesNamespace}", CodeGenerateOption.ServicesNamespace)
                .Replace("{AreasName}", areasName)
                .Replace("{ModelTypeName}", modelTypeName)
                .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 从代码模板中读取内容
        /// </summary>
        /// <param name="templateName">模板名称，应包括文件扩展名称。比如：template.txt</param>
        /// <returns></returns>
        private static string ReadTemplate(string templateName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var content = string.Empty;
            using (var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.CodeGenerator.CodeTemplates.{templateName}"))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }
            return content;
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        private static void WriteAndSave(string fileName, string content)
        {
            //实例化一个文件流--->与写入文件相关联
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                //实例化一个StreamWriter-->与fs相关联
                using (var sw = new StreamWriter(fs))
                {
                    //开始写入
                    sw.Write(content);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
        }
    }
}
