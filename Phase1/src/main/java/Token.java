import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class Token implements Comparable {
    private List<String> docs;
    private String term;

    public Token(String term) {
        this.docs = new ArrayList<>();
        this.term = term;
    }

    public void setDocs(List<String> docs) {
        this.docs = docs;
    }

    public void addToDocs(String docName) {
        this.docs.add(docName);
    }


//    @Override
//    public int compareTo(Object o1, Object o2) {
//        if (((Token) o1).getTerm().compareTo(((Token) o2).getTerm()) == 0) {
//            return 0;
//        }
//        if (((Token) o1).getTerm().compareTo(((Token) o2).getTerm()) < 0) {
//            return -1;
//        }
//
//        return 1;
//
//    }



//    @Override
//    public boolean equals(Object obj) {
//        if (this == obj) return true;
//        return false;
//    }

    public String getTerm() {
        return term;
    }

    public String getDoc() {
        return docs.get(0);
    }

    public List<String> getDocs() {
        return docs;
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
