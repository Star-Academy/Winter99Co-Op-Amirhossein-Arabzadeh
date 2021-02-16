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
        int oneDocsWordOccurrenceComparingToAnother = this.getTerm().compareTo(((DocsWordOccurrence) o).getTerm());
        if (oneDocsWordOccurrenceComparingToAnother == 0) {
            return 0;
        }
        if (oneDocsWordOccurrenceComparingToAnother < 0) {
            return -1;
        }
        return 1;
    }

    @Override
    public boolean equals(Object obj) {
        return ((DocsWordOccurrence)obj).getTerm().equals(this.getTerm()) && ((DocsWordOccurrence)obj).getDoc().equals(this.getDoc());
    }
}
