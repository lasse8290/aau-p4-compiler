//lasse lang

//defines a event
define event i2c {
    //a boolean event that when true will trigger the event
    trigger: check();
    maxInterval: 1000;
    minInterval: 10000;
    function: readI2C: returns (byte[] bytes, int byteCount);
}

//like c struct
composite person {
    
}

//like c union
union floatByte {
    
}

enum someEnum {
    a,
    b,
    c
}

//event handler for i2c
event i2c: in(byte[] bytes, int byteCount){

}

main: in (float x) returns (int a, string b) {
    
    a = (int)x;
    b = x.ToString();

    return; 
}