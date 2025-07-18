using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainServices
{
    internal class ModelTrainer
    {
        private readonly ICorpusSource _source;
        private readonly IProcessor _processor;

        public ModelTrainer(ICorpusSource source, IProcessor processor)
        {
            _source = source;
            _processor = processor;
        }

        public void ReadSliding(int windowSize)
        {
            var buffer = new char[windowSize];
            int filled = _source.ReadBlock(buffer, 0, windowSize);

            if (filled < windowSize)
                return;

            _processor.Process(buffer.AsSpan());

            int nextChar;
            while ((nextChar = _source.Read()) != -1)
            {
                buffer.AsSpan(1).CopyTo(buffer);
                buffer[windowSize - 1] = (char)nextChar;

                _processor.Process(buffer.AsSpan());
            }
        }
    }
}
