using System;

public class Bussiness_1 {

    private DatabaseFactory df;
    private Database db;
    Connection con;

    public Bussiness_1() {
        df = new DatabaseFactoryImpl_1();
        db = df.getDatabase();

        con = db.getConnection();
    }

    public void insert(string id, string code, int quality) {
        string query = "insert into product values (" + id + "," + code + "," + quality + ")";
        string stmt = con.createStatement();
        Console.WriteLine("{0}, {1}", query, stmt);
    }

}
