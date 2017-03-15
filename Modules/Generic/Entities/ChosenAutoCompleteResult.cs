namespace Application.Entity {

    /**
        This is the view model used by chosen ajaxified.
     */
    public class ChosenAutoCompleteResult {
        public string id { get; set; }
        public string text { get; set; }

        public ChosenAutoCompleteResult(string id, string text) {
            this.id = id;
            this.text = text;
        }
    }
}