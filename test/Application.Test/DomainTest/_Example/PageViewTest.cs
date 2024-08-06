using Core.Models;
using Domain.Example.PageView;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Test.DomainTest.Example
{
    [TestFixture]
    public class PageViewTest : BaseTest
    {
        private IPageViewService _pageViewSvc;

        [SetUp]
        public void Setup()
        {
            _pageViewSvc = _svcProvider.GetService<IPageViewService>();
        }

        [Test]
        public async Task TestPageCount()
        {
            Random rnd = new Random();
            var pageCount = rnd.Next(100);

            var pageReq = new PagingRequest
            {
                Length = pageCount,
                Page = 0,
                Parameters = new List<PagingParameter> { new PagingParameter { Name = nameof(ExPersonViewDto.Name), SearchValue = "" } },
                Order = new PagingOrder() { ParameterName = "Name", Dir = "asc" }
            };

            var resp = await _pageViewSvc.GetPageData(pageReq);
            var dataToCheck = resp.Value;

            if (pageCount > dataToCheck.RecordsTotal)
            {
                pageCount = dataToCheck.RecordsTotal;
            }

            Assert.That(dataToCheck.Data.Count(), Is.EqualTo(pageCount));
        }

        [Test]
        public async Task TestPageData()
        {
            Random rnd = new Random();
            var loopCount = rnd.Next(10);

            List<ExPersonViewDto> datas = new List<ExPersonViewDto>();
            for (int i = 0; i > loopCount; i++)
            {
                var pageReq = new PagingRequest
                {
                    Length = 10,
                    Page = i,
                    Parameters = new List<PagingParameter> { new PagingParameter { Name = nameof(ExPersonViewDto.Name), SearchValue = "" } },
                    Order = new PagingOrder() { ParameterName = "Name", Dir = "asc" }
                };

                var resp = await _pageViewSvc.GetPageData(pageReq);
                var dataToCheck = resp.Value;

                datas.AddRange(dataToCheck.Data);

                if (!dataToCheck.Data.Any())
                {
                    break;
                }
            }

            var dataDistinct = datas.Select(m => m.Id).Distinct();

            Assert.That(datas.Count, Is.EqualTo(dataDistinct.Count()));
        }
    }
}
