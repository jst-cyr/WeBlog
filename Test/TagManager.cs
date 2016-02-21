using System.IO;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Search;
using Sitecore.SecurityModel;
using Mod = Sitecore.Modules.WeBlog.Managers;

#if FEATURE_CONTENT_SEARCH
using Sitecore.ContentSearch;
#endif

namespace Sitecore.Modules.WeBlog.Test
{
    [TestFixture]
    [Category("TagManager")]
    public class TagManager : UnitTestBase
    {
        protected Item m_testRoot = null;
        protected Item m_blog1 = null;
        protected Item m_entry1 = null;
        protected Item m_entry2 = null;
        protected Item m_entry3 = null;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // Create test content
            using (new SecurityDisabler())
            {
              m_testContentRoot.Paste(File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\test data\tag manager content.xml")), false, PasteMode.Overwrite);
            }

            Initialize();
        }

        protected void Initialize()
        {
            // Retrieve created content items
            m_testRoot = m_testContentRoot.Axes.GetChild("blog testroot");
            m_blog1 = m_testRoot.Axes.GetChild("myblog");

            m_entry1 = m_blog1.Axes.GetDescendant("Entry1");
            m_entry2 = m_blog1.Axes.GetDescendant("Entry2");
            m_entry3 = m_blog1.Axes.GetDescendant("Entry3");

            // rebuild the WeBlog search index (or some aspects of TagManager won't work)
#if FEATURE_CONTENT_SEARCH
            var index = ContentSearchManager.GetIndex(Settings.SearchIndexName);
            index.Rebuild();
#else
            var index = SearchManager.GetIndex(Settings.SearchIndexName);
            index.Rebuild();
#endif
        }

        [Test]
        public void GetAllTags_Blog1()
        {
            var tags = new Mod.TagManager().GetAllTags(new Items.WeBlog.BlogHomeItem(m_blog1));
            var tagA = tags.SingleOrDefault(tag => tag.Name == "taga");
            var tagB = tags.SingleOrDefault(tag => tag.Name == "tagb");
            var tagC = tags.SingleOrDefault(tag => tag.Name == "tagc");

            //present
            Assert.IsNotNull(tagA, "taga not found");
            Assert.IsNotNull(tagB, "tagb not found");
            Assert.IsNotNull(tagC, "tagc not found");

            //counts
            Assert.AreEqual(3, tagA.Count, "taga count is wrong");
            Assert.AreEqual(1, tagB.Count, "tagb count is wrong");
            Assert.AreEqual(2, tagC.Count, "tagc count is wrong");
            
            //order
            Assert.AreEqual(tagA, tags[0], "taga index is wrong");
            Assert.AreEqual(tagC, tags[1], "tagc index is wrong");
            Assert.AreEqual(tagB, tags[2], "tagb index is wrong");
        }

        [Test]
        public void GetAllTags_EntryItem()
        {
            var tags = new Mod.TagManager().GetAllTags(new Items.WeBlog.BlogHomeItem(m_entry1));
            //entry is part of blog w/ 3 tags
            Assert.AreEqual(3, tags.Length);
        }

        [Test]
        public void GetAllTags_Null()
        {
            var tags = new Mod.TagManager().GetAllTags(null);
            Assert.AreEqual(0, tags.Length);
        }

    }
}