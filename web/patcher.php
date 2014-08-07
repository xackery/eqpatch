<?php

$baseDirectory = dirname(__FILE__);
function recursiveDirectoryIterator ($directory = null, $dataList) {
    global $baseDirectory, $expansionDirectory;
    $iterator = new \DirectoryIterator ( $directory );

    foreach ( $iterator as $info ) {
        if ($info->isFile ()) {
            $webPath = str_replace($baseDirectory, "", $info->getPath());
            $relativePath = substr($webPath, strlen($expansionDirectory));
            $relativePath = str_replace("/", "\\", $relativePath);
            
            $dataList[] = array('fileName' => $info->getFilename(), 'md5' => md5_file($info->getPathname()), 'pathName' => $relativePath, 'webPath' => 'http://'.$_SERVER['HTTP_HOST'].dirname($_SERVER['PHP_SELF'])."$webPath/".$info->getFilename());
        } elseif (!$info->isDot ()) {
            $list = recursiveDirectoryIterator(
                        $directory.DIRECTORY_SEPARATOR.$info->__toString (), array());
            if(!empty($dataList))
                $dataList = array_merge_recursive($dataList, $list);
            else {
                $dataList = $list;
            }
        }
    }
    return $dataList;
}

if (!isset($_GET['client_id'])) die("No client_id set");
$client_id = $_GET['client_id'];

if ($client_id == 0) $expansionDirectory .= "/titanium";
else if ($client_id == 1) $expansionDirectory .= "/sof";
else if ($client_id == 2) $expansionDirectory .= "/underfoot";
else if ($client_id == 3) $expansionDirectory .= "/rof";
else die("Invalid client_id set: $client_id");

$fileList = recursiveDirectoryIterator($baseDirectory . $expansionDirectory, array());
echo json_encode($fileList);