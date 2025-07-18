using Domain.Interfaces;

namespace Domain.DomainServices
{
    public class ModelTrainer
    {
        private readonly ICorpusSource _source;
        private readonly IProcessor _processor;

        public ModelTrainer(ICorpusSource source, IProcessor processor)
        {
            _source = source;
            _processor = processor;
        }

        public void TrainModel(int ngram)
        {
            var buffer = new char[ngram];
            int filled = _source.ReadBlock(buffer, 0, ngram);

            if (filled < ngram)
                return;

            _processor.Process(buffer.AsSpan());

            int nextChar;
            while ((nextChar = _source.Read()) != -1)
            {
                buffer.AsSpan(1).CopyTo(buffer);
                buffer[ngram - 1] = (char)nextChar;

                _processor.Process(buffer.AsSpan());
            }

            // Process any remaining buffer content after EOF
            for (int i = 1; i < ngram; i++)
            {
                buffer.AsSpan(1).CopyTo(buffer);
                buffer[ngram - 1] = '\0';

                _processor.Process(buffer.AsSpan());
            }
        }
    }
}
