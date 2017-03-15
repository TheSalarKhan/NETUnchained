namespace Application.Models {

    /**
        This model is for holding binary data. Specially things like BASE-64 encoded images etc.
     */
    public class BinaryData : BaseModel {
        public BinaryData() {
            
        }
        
        public BinaryData(byte[] data) : base(null) {
            this.Data = data;
        }
        public byte[] Data { get; set; }
    }
}