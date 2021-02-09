public class MyToken implements Comparable {
    private String doc;
    private String term;

    public MyToken(String term) {
        this.term = term;
    }

    public String getDoc() {
        return doc;
    }


    public String getTerm() {
        return term;
    }


    public void setDoc(String doc) {
        this.doc = doc;
    }

    @Override
    public int compareTo(Object o) {
        if (this.getTerm().compareTo(((MyToken) o).getTerm()) == 0) {
            return 0;
        }
        if (this.getTerm().compareTo(((MyToken) o).getTerm()) < 0) {
            return -1;
        }

        return 1;
    }
}
