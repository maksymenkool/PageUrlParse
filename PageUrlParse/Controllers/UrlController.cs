using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PageUrlParse.Models;
using HtmlAgilityPack;

namespace PageUrlParse.Controllers
{
    public class UrlController : ApiController
    {
        private UrlDBContext db = new UrlDBContext();

        // GET: api/Url
        public IQueryable<Url_tbl> GetUrlPages()
        {
            return db.UrlPages;
        }

        // GET: api/Url/5
        [ResponseType(typeof(Url_tbl))]
        public IHttpActionResult GetUrl_tbl(int id)
        {
            Url_tbl url_tbl = db.UrlPages.Find(id);
            if (url_tbl == null)
            {
                return NotFound();
            }

            url_tbl.PageLists_tbl = db.PageLists.Where(p => p.UrlId == url_tbl.IdUrl).OrderByDescending(p => p.Speed).ToList();
            return Ok(url_tbl);
        }

        // POST: api/Url
        [ResponseType(typeof(Url_tbl))]
        public IHttpActionResult AddUrl(Url_tbl url_tbl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<string> hrefs = new List<string>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url_tbl.NameUrl);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a");
            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    hrefs.Add(node.GetAttributeValue("href", null));
                }
            }

            List<decimal> speeds = new List<decimal>();
            foreach (string href in hrefs)
            {
                DateTime before = DateTime.Now;

                try
                {
                    WebRequest webRequest = HttpWebRequest.Create(href);
                    HttpWebResponse httpResponse = (HttpWebResponse)webRequest.GetResponse();

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        TimeSpan timeSpan = DateTime.Now.Subtract(before);
                        speeds.Add((decimal)(timeSpan.TotalMilliseconds));

                        PageList_tbl page = new PageList_tbl();
                        page.NamePage = href;
                        page.Speed = (decimal)(timeSpan.TotalMilliseconds);
                        page.UrlId = (int)(url_tbl.IdUrl);
                        db.PageLists.Add(page);
                    }
                }
                catch (Exception)
                {

                }
            }
            decimal min = speeds.Min();
            decimal max = speeds.Max();
            url_tbl.MinSpeed = min;
            url_tbl.MaxSpeed = max;

            db.UrlPages.Add(url_tbl);
            db.SaveChanges();

             return CreatedAtRoute("DefaultApi", new { id = url_tbl.IdUrl }, url_tbl);
        }

        // DELETE: api/Url/5
        [ResponseType(typeof(Url_tbl))]
        public IHttpActionResult DeleteUrl_tbl(int id)
        {
            Url_tbl url_tbl = db.UrlPages.Find(id);
            if (url_tbl == null)
            {
                return NotFound();
            }

            var urls = db.PageLists.Where(p => p.UrlId == url_tbl.IdUrl);
            foreach (PageList_tbl pl in urls)
            {
                db.PageLists.Remove(pl);
            }

            db.UrlPages.Remove(url_tbl);
            db.SaveChanges();

            return Ok(url_tbl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
