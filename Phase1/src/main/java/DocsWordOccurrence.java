public class DocsWordOccurrence implements Comparable, WordOccurrence {
    private String doc;
    private String term;

    public DocsWordOccurrence(String term, String doc) {
        this.doc = doc;
        this.term = term;
    }

    public String getDoc() {
        return doc;
    }


    public String getTerm() {
        return term;
    }


    @Override
    public int compareTo(Object o) {
//        if (!o.getClass().equals(MyToken.class)){
//            return
//        }
        if (this.getTerm().compareTo(((DocsWordOccurrence) o).getTerm()) == 0) {
            return 0;
        }
        if (this.getTerm().compareTo(((DocsWordOccurrence) o).getTerm()) < 0) {
            return -1;
        }
        return 1;
    }
}
