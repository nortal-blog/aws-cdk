exports.handler = async (event, context) => {
    // TODO implement
    const result = "Hello from Lambda named " + context.functionName;
    
    const response = {
        statusCode: 200,
        body: result
    };
    return response;
};
