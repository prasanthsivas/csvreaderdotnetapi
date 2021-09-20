using csvreaderdotnetapi.Models.Repository;
using System.Collections.Generic;
using System.Linq;

namespace csvreaderdotnetapi.Models.DataManager
{
    public class CaptionManager : IDataRepository<Caption>
    {
        readonly CaptionContext _captionContext;

        public CaptionManager(CaptionContext captionContext)
        {
            _captionContext = captionContext;
        }

        public void AddRange(IEnumerable<Caption> entityRange)
        {
            _captionContext.Captions.AddRange(entityRange);
            _captionContext.SaveChanges();
        }

        void IDataRepository<Caption>.Add(Caption caption)
        {
            _captionContext.Captions.Add(caption);
            _captionContext.SaveChanges();
        }

        void IDataRepository<Caption>.Delete(Caption caption)
        {
            _captionContext.Captions.Remove(caption);
            _captionContext.SaveChanges();
        }

        Caption IDataRepository<Caption>.Get(long id)
        {
            return _captionContext.Captions.FirstOrDefault(c => c.captionId == id);
        }

        IEnumerable<Caption> IDataRepository<Caption>.GetAll()
        {
            return _captionContext.Captions.ToList();
        }

        void IDataRepository<Caption>.Update(Caption caption, Caption entity)
        {
            caption.comment = entity.comment;
            caption.imageName = entity.imageName;
            caption.commentNumber = entity.commentNumber;

            _captionContext.SaveChanges();
        }
    }
}
