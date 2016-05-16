public class DatabaseImpl_1 : Database {

    private Connection con;
    //private string stmt;
    //private string rset; //

    public DatabaseImpl_1() {
        con = new Connection();
        //stmt = con.createStatement();
    }

    public Connection getConnection() {
        return con;
    }

}
