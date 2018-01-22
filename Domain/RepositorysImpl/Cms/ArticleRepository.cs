namespace Domain.RepositorysImpl.Cms
{
    using System;
	using Domain.IRepositorys;
	using Domain.IRepositorys.Cms;
	using Domain.Models.Cms;
    public class ArticleRepository : BaseRepository<Article, String>, IArticleRepository
    {
        public ArticleRepository(DefaultDbContext dbContext) : base(dbContext)
        {
			// 这是RepositoryTemplate.txt代码模板生成（添加方法前，请先对接口IArticleRepository进行修订）
        }
    }
}
