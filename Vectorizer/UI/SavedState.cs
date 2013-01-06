using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Vectorizer.Logic.Picture;

namespace Vectorizer.UI
{
    [Serializable]
    class SavedState
    {
        readonly Bitmap image;
        readonly VectorPicture vectorPicture;

        public SavedState(Bitmap image, VectorPicture vectorPicture)
        {
            this.image = image;
            this.vectorPicture = vectorPicture;
        }

        public Bitmap Image
        {
            get { return image; }
        }

        public VectorPicture VectorPicture
        {
            get { return vectorPicture; }
        }
    }
}
