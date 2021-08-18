using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CheckArticleService: ICheckArticleService
    {
        private IArticle article;
        public CheckArticleService(IArticle _article)
        {
            article = _article;
        }

        public bool HasHeading()
        {

            if (article == null) return false; 
            //if (article.Name == null)
            //{
            //    return false;
            //}
            return true;
        }

        public bool HasAuthor()
        {
            if (article == null) return false;

            //if (article.Author == null)
            //{
            //    return false;
            //}
            return true;
        }
        public bool HasText()
        {
            if (article == null) return false;

            if (article.Text.NumOfSigns == 0)
            {
                return false;
            }
            return true;
        }
        public bool IsOfSetVolume(uint volume)
        {
            if (this.HasText())
            {
                if (article.Text.NumOfSigns > volume)
                {
                    return false;
                }
                //if (article.Text.NumOfSigns < volume)
                //{
                //    return false;
                //}
                return true;
            }
            return false;
        }
        public bool ContainsWords(string[] words)
        {
            if (this.HasText())
            {
                foreach (var word in words)
                {
                    if (article.Text.Text.Contains(word))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
