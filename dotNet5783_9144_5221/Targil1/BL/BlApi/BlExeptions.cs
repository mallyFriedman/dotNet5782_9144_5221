

namespace BlApi
{
    public class BlObjectNotFoundException : Exception
    {
        public BlObjectNotFoundException(DalApi.EntityNotFoundException? inner = null) : base("entity not found", inner) { }
        public override string Message =>
                        "entity not found";
    }

    public class BlIdNotValidException : Exception
    {
        //public BlIdNotValidException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "id not valid";

    }

    public class BlObjectNotValidException : Exception
    {
        //public BlIdNotValidException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "object not valid";

    }

    public class BlCannotChangeTheStatusException : Exception
    {
        //public BlCannotChangeTheStatusException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "cannot change the status to the requier status";
    }

    public class BlOutOfStockException : Exception
    {
        //public BlOutOfStockException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "the product out of stock";
    }
    public class BlCartIsEmptyException : Exception
    {
        //public BlCartIsEmptyException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "your cart is empty";

    }
    public class BlDetailsNotValidException : Exception
    {
        //public BlDetailsNotValidException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "details not valid";

    }
    public class BlUnknownException : Exception
    {
        //public BlUnknownException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "unknown exception";
    }
    public class BlTryFailed : Exception
    {
        //public BlUnknownException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "try failed";
    }

    public class BlInStockMustBeANumber : Exception
    {
        //public BlUnknownException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "In stock must be a number!";
    }

    public class BlPriceMustBeANumber : Exception
    {
        //public BlUnknownException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "Price must be a number!";
    }
    public class BlNoOrderToUpdateException : Exception
    {
        //public BlUnknownException(string message) :
        //                                base(message)
        //{
        //}
        public override string Message =>
                       "There is no order to update!";
    }
}


