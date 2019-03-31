function net=adamcnn()
imageSize = [360 640 3];

layers = [
    imageInputLayer(imageSize,'Name','input')
    
    convolution2dLayer(3,8,'Padding','same','Name','conv_1')
    batchNormalizationLayer('Name','BN_1')
    reluLayer('Name','relu_1')
    
    maxPooling2dLayer(2,'Stride',2,'Name','Pool_1')
    
    convolution2dLayer(3,16,'Padding','same','Name','conv_2')
    batchNormalizationLayer('Name','BN_2')
    reluLayer('Name','relu_2')
    
    maxPooling2dLayer(2,'Stride',2,'Name','Pool_2')
    
    convolution2dLayer(3,32,'Padding','same','Name','conv_3')
    batchNormalizationLayer('Name','BN_3')
    reluLayer('Name','relu_3')
    dropoutLayer(0.5,'Name','Drop_1')
    fullyConnectedLayer(16,'Name','Fc')
    softmaxLayer('Name','Softmax')
    classificationLayer('Name','ClassOutput')];

digitDatasetPath = fullfile('E:\okul\bitime dökümanlar\cnn2\train');
imds = imageDatastore(digitDatasetPath, ...
    'IncludeSubfolders',true, ...
    'LabelSource','foldernames');

digitDatasetPath2 = fullfile('E:\okul\bitime dökümanlar\cnn2\test');
imds2 = imageDatastore(digitDatasetPath2, ...
    'IncludeSubfolders',true, ...
    'LabelSource','foldernames');

options = trainingOptions('adam', ...
    'MaxEpochs',32,...
    'Shuffle','every-epoch',...
    'InitialLearnRate',1e-4, ...
    'Verbose',false, ...
    'MiniBatchSize',64,...
    'Plots','training-progress');

net = trainNetwork(imds,layers,options);
YPred = classify(net,imds2);
YTest = imds2.Labels;
accuracy = sum(YPred == YTest)/numel(YTest)