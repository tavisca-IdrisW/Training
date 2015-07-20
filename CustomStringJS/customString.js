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


  if(fromIndex > this.length || toIndex > this.length)
  {
    return { 
      name: "Exception", 
      message: "Source index  or destination index is larger than Source String.", 
      toString: function(){
        return this.name + ": " + this.message;
      } 
    }
  }

  console.log(fromIndex);
  console.log(toIndex);
  if (!toIndex || !fromIndex )
  {
    fromIndex = arguments[0];
    toIndex = this.length;
  }

  var subString ="";
  for (var index = fromIndex ; index < toIndex; index++) {
    subString += this[index];
  }

  return subString;
};

String.prototype.Concat = function () {
  var appnedString = '';

  for (var index = 0; index < arguments.length; index++) {
    appnedString += arguments[index];
  }
  return this + appnedString;
};