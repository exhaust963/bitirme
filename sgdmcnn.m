function net=sgdmcnn()
imageSize = [360 640 3];

layers = [
    imageInputLayer(imageSize)
    
    convolution2dLayer(3,8,'Padding','same')
    batchNormalizationLayer
    reluLayer   
    
    maxPooling2dLayer(2,'Stride',2)
    
    convolution2dLayer(3,16,'Padding','same')
    batchNormalizationLayer
    reluLayer   
    
    maxPooling2dLayer(2,'Stride',2)
    
    convolution2dLayer(3,32,'Padding','same')
    batchNormalizationLayer
    reluLayer   
    
    dropoutLayer(0.5)
    fullyConnectedLayer(16)
    softmaxLayer
    classificationLayer];

digitDatasetPath = fullfile('E:\okul\bitime dökümanlar\train');
imds = imageDatastore(digitDatasetPath, ...
    'IncludeSubfolders',true, ...
    'LabelSource','foldernames');

digitDatasetPath2 = fullfile('E:\okul\bitime dökümanlar\test');
imds2 = imageDatastore(digitDatasetPath2, ...
    'IncludeSubfolders',true, ...
    'LabelSource','foldernames');

options = trainingOptions('sgdm', ...
    'MaxEpochs',8,...
    'Shuffle','every-epoch',...
    'InitialLearnRate',1e-4, ...
    'Verbose',false, ...
    'MiniBatchSize',64,...
    'Plots','training-progress');

net = trainNetwork(imds,layers,options);

YPred = classify(net,imds2);
YTest = imds2.Labels;
accuracy = sum(YPred == YTest)/numel(YTest)