using Microsoft.Extensions.Logging;

namespace Capital.GSG.FX.Utils.Core.Logging
{
    public class GSGLoggerFactory : ILoggerFactory
    {
        private readonly LoggerFactory _loggerFactory;

        private static GSGLoggerFactory instance;

        public static GSGLoggerFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new GSGLoggerFactory();

                return instance;
            }
        }

        private GSGLoggerFactory()
        {
            _loggerFactory = new LoggerFactory();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggerFactory.CreateLogger(categoryName);
        }

        public ILogger CreateLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }

        public void AddProvider(ILoggerProvider provider)
        {
            _loggerFactory.AddProvider(provider);
        }

        public void Dispose()
        {
            _loggerFactory.Dispose();
        }
    }
}
