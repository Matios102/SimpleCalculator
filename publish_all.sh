#!/bin/bash

architectures=("win-x64" "win-arm64" "osx-x64" "osx-arm64" "linux-x64" "linux-arm64")

SOURCE_FOLDER="source"

for arch in "${architectures[@]}"; do
    TARGET_FOLDER="../Release/Mateusz.Osik-$arch"

    echo "Publishing for $arch..."
    dotnet publish -r $arch -c Release --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o "$TARGET_FOLDER"

    echo "Removing .pdb files from $TARGET_FOLDER..."
    find "$TARGET_FOLDER" -name "*.pdb" -type f -delete

    echo "Copying source code to $TARGET_FOLDER/source..."
    cp -r $SOURCE_FOLDER "$TARGET_FOLDER/"

    echo "Creating ZIP archive for $TARGET_FOLDER..."
    zip -r "$TARGET_FOLDER.zip" "$TARGET_FOLDER" -x "*.DS_Store" "__MACOSX/*"

    echo "Build and source copied & zipped for $arch in $TARGET_FOLDER.zip"
done

echo "All builds completed and zipped!"
