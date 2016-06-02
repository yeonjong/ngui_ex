public class DatabaseFactoryImpl_1 : DatabaseFactory {

    private Database db;

    public Database getDatabase() {
        db = new DatabaseImpl_1();
        return db;
    }

}
