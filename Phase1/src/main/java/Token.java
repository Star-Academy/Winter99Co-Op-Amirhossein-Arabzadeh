public interface Token {
    String getDoc();


    String getTerm();


    void setDoc(String doc);

    int compareTo(Object o);
}
