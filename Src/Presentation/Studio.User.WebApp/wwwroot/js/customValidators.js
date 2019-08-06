$.validator.unobtrusive.adapters.add('requiredif', ['dependentproperty', 'targetvalue'], function (options) {
    options.rules['requiredif'] = {
        dependentproperty: options.params['dependentproperty'],
        targetvalue: options.params['targetvalue']
    };
    options.messages['requiredif'] = options.message;
});

$.validator.addMethod('requiredif', function (value, element, parameters) {

    var dependentProperty = '#' + parameters['dependentproperty'];
    var targetvalue = parameters['targetvalue'];
    targetvalue = (targetvalue == null ? '' : targetvalue).toString();

    var dependentControl = $(dependentProperty);
    var dependentValue = dependentControl.val();

    // if the condition is true, reuse the existing required field validator functionality
    if (targetvalue.toUpperCase() === dependentValue.toUpperCase()) {
        return $.validator.methods.required.call(this, value, element, parameters);
    }

    return true;
});