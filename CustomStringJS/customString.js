String.prototype.Substr = function (fromIndex, toIndex) {

  if(fromIndex > toIndex)
  {
    return { 
      name: "Exception", 
      message: "Source index cannot be  larger than destination.", 
      toString: function(){
        return this.name + ": " + this.message;
      } 
    }
  }


  if(fromIndex > this.length - 2 || toIndex > this.length - 2 )
  {
    return { 
      name: "Exception", 
      message: "Source index  or destination index is larger than Source String.", 
      toString: function(){
        return this.name + ": " + this.message;
      } 
    }
  }

  for (var index = fromIndex ; index < toIndex; index++) {
    newSubString += this[index];
  }

  return newSubString;
};

String.prototype.Concat = function () {
  var appnedString = '';
  for (var index = 0; index < arguments.length; index++) {
    appnedString += arguments[index];
  }
  return this + appnedString;
};