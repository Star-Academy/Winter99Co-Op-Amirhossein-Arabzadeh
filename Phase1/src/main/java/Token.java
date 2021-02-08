public class Token implements Comparable {
    private String doc;
    private String term;

    public Token(String term) {
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
        if (this.getTerm().compareTo(((Token) o).getTerm()) == 0) {
            return 0;
        }
        if (this.getTerm().compareTo(((Token) o).getTerm()) < 0) {
            return -1;
        }

        return 1;
    }
}
