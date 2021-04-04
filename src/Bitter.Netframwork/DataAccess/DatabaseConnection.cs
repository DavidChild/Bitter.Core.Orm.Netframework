namespace Bitter.Core
{
    public struct DatabaseConnection
    {
        private string connectionString;
        private DatabaseType databaseType;
        private int version;
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
            set
            {
                this.connectionString = value;
            }
        }
        public int Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
            }
        }
        public DatabaseType DatabaseType
        {
            get
            {
                return this.databaseType;
            }
            set
            {
                this.databaseType = value;
            }
        }
    }
}