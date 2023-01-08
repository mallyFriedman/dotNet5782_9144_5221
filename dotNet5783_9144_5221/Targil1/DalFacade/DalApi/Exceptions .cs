namespace DalApi
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string ex = "another") { cusmessage = ex; }

        public string cusmessage;
        public override string Message =>
                        cusmessage;

    }
    public class EntityDuplicateException : Exception
    {
        public override string Message =>
                    "Entity Duplicate";

    }
    public class IdNotValidException : Exception
    {
        public override string Message =>
                       "id not valid";

    }


    //Exceptions for the xml:

    public class FileErrorException : Exception
    {
        public override string Message =>
                       "error in file type";

    }
}

