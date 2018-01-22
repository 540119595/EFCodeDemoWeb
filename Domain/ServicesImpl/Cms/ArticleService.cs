namespace Domain.ServicesImpl.Cms
{
    using System;
	using Domain.IRepositorys.Cms;
	using Domain.IServices;
	using Domain.IServices.Cms;
	using Domain.Models.Cms;
    public class ArticleService : BaseService<Article, String>, IArticleService
    {
        public ArticleService(IArticleRepository repository) : base(repository)
        {
			// 这是ServiceTemplate代码模板生成（添加方法前，请先对接口IArticleService进行修订）
        }
    }
}

