public class Singleton {

    private static Singleton single;

    static Singleton() {
        single = new Singleton();
    }

    private Singleton() { }

    public static Singleton getSingleton() {
        return single;
    }

}