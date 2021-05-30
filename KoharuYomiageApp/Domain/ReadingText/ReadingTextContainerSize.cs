using System;

namespace KoharuYomiageApp.Domain.ReadingText
{
    public record ReadingTextContainerSize
    {
        public ReadingTextContainerSize(int size)
        {
            if (size is <=0)
            {
                throw new ArgumentException("Invalid container size");
            }

            Value = size;
        }

        public int Value { get; }
    }
}
