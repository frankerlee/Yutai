using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class EsriUtils
    {
        #region public enums

        public enum LayerSelectableOverrideEnum

        {
            NO_OVERRIDE,
            ALLWAYS_SELECTABLE,
            NEVER_SELECTABLE
        }


        public enum HideLayerOverrideEnum

        {
            NO_OVERRIDE,
            ALLWAYS_HIDE,
            NEVER_HIDE
        }


        public enum LayerVisibleOverrideEnum

        {
            NO_OVERRIDE,
            ALLWAYS_VISIBLE,
            NEVER_VISIBLE
        }


        public enum SelectionModifierEnum

        {
            NO_MODIFIER,
            ADD_TO_SELECTION,
            SUBTRACT_FROM_SELECTION
        }

        #endregion

        #region public functions

        /// <summary>Displays a file open dialog, prompting user to select a map document.</summary>
        /// <returns>String value representing the selected file or null if user clicked cancel.</returns>	
        public static string PromptForMapDocument()
        {
            // create instance of open file dialog and customize it to work with this application's doc types
            System.Windows.Forms.OpenFileDialog mapFileDialog = new System.Windows.Forms.OpenFileDialog();
            mapFileDialog.Title = "Open Map Document";
            mapFileDialog.Filter =
                "ArcMap Documents (*.mxd)|*.mxd|ArcMap Templates (*.mxt)|*.mxt|Published Maps (*.pmf)|*.pmf|All Supported Map Formats|*.mxd;*.mxt;*.pmf";
            mapFileDialog.FilterIndex = 1;
            mapFileDialog.RestoreDirectory = false;
            mapFileDialog.Multiselect = false;
            // now show the dialog and return a file name if the user closed the dialog by clicking OK
            if (mapFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return mapFileDialog.FileName.ToString();
            else
                return null;
        }

        #region functions for finding, loading and and cloning layers

        /// <summary>This method is used to return all layers in a given map.</summary>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <param name='includeContainerObjects'>True or false, indicating if containers (like grouplayers) will be returned in addition to their children.</param>
        /// <returns>An array of layer objects</returns>
        public static ILayer[] GetAllLayers(IMap esriMap, bool includeContainerObjects)
        {
            // get all the layers in map
            System.Collections.ArrayList mapLayersArrayList = EsriUtils.getAllLayers(esriMap, includeContainerObjects);
            // transform array list to array and return it
            return EsriUtils.layerArrayListToArray(mapLayersArrayList);
        }


        /// <summary>This method is used to return all layers in a map that are selectable by the user</summary>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <returns>An array of layer objects</returns>
        public static ILayer[] GetSelectableLayers(IMap esriMap)
        {
            // get all layers in the map
            System.Collections.ArrayList tempArrayList = EsriUtils.getAllLayers(esriMap, false);
            // create an empty array list to place layers that match criteria
            System.Collections.ArrayList finalArrayList = new System.Collections.ArrayList();
            if (tempArrayList != null)
            {
                // enumerate through array list and do a string compare on source table name and input string
                System.Collections.IEnumerator tempArrayEnumerator = tempArrayList.GetEnumerator();
                while (tempArrayEnumerator.MoveNext())
                {
                    // get the layer object from array list
                    ILayer esriLayer = (ILayer) tempArrayEnumerator.Current;
                    if (EsriUtils.LayerIsValid(esriLayer) == true)
                    {
                        // determine if it is a feature layer (which could be selectable)
                        if (esriLayer is IFeatureLayer)
                        {
                            // get feature layer interface and check selectable property
                            IFeatureLayer esriFeatureLayer = (IFeatureLayer) esriLayer;
                            if (esriFeatureLayer.Selectable == true)
                            {
                                // add the layer to return array list
                                finalArrayList.Add(esriLayer);
                            }
                        }
                    }
                }
            }
            return EsriUtils.layerArrayListToArray(finalArrayList);
        }


        /// <summary>This method is used to return all layers in a map that are visible</summary>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <returns>An array of layer objects</returns>
        public static ILayer[] GetVisibleLayers(IMap esriMap)
        {
            // get all layers in the map
            System.Collections.ArrayList tempArrayList = EsriUtils.getAllLayers(esriMap, true);
            // create an empty array list to place layers that match criteria
            System.Collections.ArrayList finalArrayList = new System.Collections.ArrayList();
            if (tempArrayList != null)
            {
                // enumerate through array list and do a string compare on source table name and input string
                System.Collections.IEnumerator tempArrayEnumerator = tempArrayList.GetEnumerator();
                while (tempArrayEnumerator.MoveNext())
                {
                    // get the layer object from array list
                    ILayer esriLayer = (ILayer) tempArrayEnumerator.Current;
                    if (EsriUtils.LayerIsValid(esriLayer) == true)
                    {
                        // determine if it is a feature layer (which could be selectable)
                        if (esriLayer.Visible == true)
                        {
                            // add the layer to return array list
                            finalArrayList.Add(esriLayer);
                        }
                    }
                }
            }
            return EsriUtils.layerArrayListToArray(finalArrayList);
        }


        /// <summary>This method returns layers based on their user given name within a map</summary>
        /// <param name='layerName'>Layer name that will be used to filter returned layers.</param>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <param name='ignoreCase'>True or false indicating whether case will be considered when evaluating the layer name</param>
        /// <returns>An array of layer objects</returns>
        public static ILayer[] GetAllLayersByName(string layerName, IMap esriMap)
        {
            return EsriUtils.GetAllLayersByName(layerName, esriMap, true);
        }

        // overload which exposes the "ignoreCase" parameter.
        public static ILayer[] GetAllLayersByName(string layerName, IMap esriMap, bool ignoreCase)
        {
            // get all layers in the map
            System.Collections.ArrayList tempArrayList = EsriUtils.getAllLayers(esriMap, true);
            // create an empty array list to place layers that match criteria
            System.Collections.ArrayList finalArrayList = new System.Collections.ArrayList();
            if (tempArrayList != null)
            {
                // enumerate through array list and do a string compare on source table name and input string
                System.Collections.IEnumerator tempArrayEnumerator = tempArrayList.GetEnumerator();
                int layerMatchCount = 0;
                while (tempArrayEnumerator.MoveNext())
                {
                    // get the layer object from array list
                    ILayer esriLayer = (ILayer) tempArrayEnumerator.Current;
                    // return the layer's table name
                    string currentLayerName = esriLayer.Name;
                    // compare it with the input string (case insensitive)
                    if (System.String.Compare(currentLayerName, layerName, ignoreCase) == 0)
                    {
                        // when strings match, add to other array list and increment counter
                        finalArrayList.Add(esriLayer);
                        layerMatchCount++;
                    }
                }
                // clear the array list containing all the map's layers
                tempArrayList.Clear();
                tempArrayEnumerator = null;
                tempArrayList = null;
            }
            // now transform array list into an array and return it
            return EsriUtils.layerArrayListToArray(finalArrayList);
        }


        /// <summary>This method is used to return one layer based on it's user given name within a map.</summary>
        /// <param name='layerName'>Layer name that will be used to filter returned layers.</param>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <param name='ignoreCase'>True or false indicating whether case will be considered when evaluating the layer name</param>
        /// <returns>An esri ILayer object</returns>
        public static ILayer GetOneLayerByName(string layerName, IMap esriMap)
        {
            return EsriUtils.GetOneLayerByName(layerName, esriMap, true);
        }

        // overload which exposes the "ignoreCase" parameter.
        public static ILayer GetOneLayerByName(string layerName, IMap esriMap, bool ignoreCase)
        {
            // get array of all layers based on the input table name
            ILayer[] esriLayers = EsriUtils.GetAllLayersByName(layerName, esriMap, ignoreCase);
            if (esriLayers == null)
                // return null of array is null
                return null;
            else
            {
                // determine if array contains at least one member
                if (esriLayers.Length > 0)
                    // return the 1st member in array
                    return esriLayers[0];
                else
                    // empty array, return null
                    return null;
            }
        }


        /// <summary>This method returns layers based on the existence of a field whose name matches the input string.</summary>
        /// <param name='fieldName'>Field name that will be used to filter returned layers.</param>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <returns>An array of layer objects</returns>
        public static ILayer[] GetAllLayersByFieldName(string fieldName, IMap esriMap)
        {
            return EsriUtils.GetAllLayersByFieldName(fieldName, esriMap, false);
        }

        /// <summary>This method returns layers based on the existence of a field whose name matches the input string.</summary>
        /// <param name='fieldName'>Field name that will be used to filter returned layers.</param>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <param name='withSelectionOnly'>True or false indicating whether layers must have one or more features selected to be considered.</param>
        /// <returns>An array of layer objects</returns>
        public static ILayer[] GetAllLayersByFieldName(string fieldName, IMap esriMap, bool withSelectionOnly)
        {
            // get all layers in the map
            System.Collections.ArrayList tempArrayList = EsriUtils.getAllLayers(esriMap, false);
            // create an empty array list to place layers that match criteria
            System.Collections.ArrayList finalArrayList = new System.Collections.ArrayList();
            if (tempArrayList != null)
            {
                // enumerate through array list and do a string compare on source table name and input string
                System.Collections.IEnumerator tempArrayEnumerator = tempArrayList.GetEnumerator();
                while (tempArrayEnumerator.MoveNext())
                {
                    // get the layer object from array list
                    ILayer esriLayer = (ILayer) tempArrayEnumerator.Current;
                    if (esriLayer is IFeatureLayer)
                    {
                        // drill down to layer's feature class for access to underlying rows & columns
                        IFeatureLayer esriFeatureLayer = (IFeatureLayer) esriLayer;
                        IFeatureClass esriFeatureClass = esriFeatureLayer.FeatureClass;
                        if (esriFeatureClass != null)
                        {
                            int esriFieldIndex = 0;
                            bool fieldFound = false;
                            // get the field collection and count of fields in collection
                            IFields esriFieldCollection = esriFeatureClass.Fields;
                            int esriFieldCount = esriFieldCollection.FieldCount;
                            // loop through each field to determine if it has one with the same name as our search criteria
                            for (esriFieldIndex = 0; esriFieldIndex != esriFieldCount; esriFieldIndex++)
                            {
                                IField esriField = esriFieldCollection.get_Field(esriFieldIndex);
                                // compare this field's name to the search string (case insensitive)
                                if (System.String.Compare(esriField.Name, fieldName, true) == 0)
                                {
                                    // set flag indicating this layer does have field we are looking for and break out of loop
                                    fieldFound = true;
                                    break;
                                }
                            }
                            // check if flag indicates a field was found
                            if (fieldFound == true)
                            {
                                // check if we are supposed to additionally filter on if the layer has a selection or not
                                if (withSelectionOnly == true)
                                {
                                    if (esriLayer is ITableSelection)
                                    {
                                        // check for the number of selection rows
                                        ITableSelection esriTableSelection = (ITableSelection) esriLayer;
                                        if (esriTableSelection.SelectionSet.Count > 0)
                                        {
                                            // add layer to the output array list
                                            finalArrayList.Add(esriLayer);
                                        }
                                    }
                                }
                                else
                                {
                                    // we don't care about if selection exists or not, add layer to the output array list
                                    finalArrayList.Add(esriLayer);
                                }
                            }
                        }
                    }
                }
            }
            // transform array list into an array of layers and return it
            return EsriUtils.layerArrayListToArray(finalArrayList);
        }


        /// <summary>This method is used to return one layer based on it's underlying database table name.</summary>
        /// <param name='fieldName'>Field name that will be used to filter returned layers.</param>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <returns>An esri ILayer object</returns>
        public static ILayer GetOneLayerByFieldName(string fieldName, IMap esriMap)
        {
            return EsriUtils.GetOneLayerByFieldName(fieldName, esriMap, false);
        }

        /// <summary>This method is used to return one layer based on it's underlying database table name.</summary>
        /// <param name='fieldName'>Field name that will be used to filter returned layers.</param>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <param name='withSelectionOnly'>True or false indicating whether layers must have one or more features selected to be considered.</param>
        /// <returns>An esri ILayer object</returns>
        public static ILayer GetOneLayerByFieldName(string fieldName, IMap esriMap, bool withSelectionOnly)
        {
            // get array of all layers based on the input table name
            ILayer[] esriLayers = EsriUtils.GetAllLayersByFieldName(fieldName, esriMap, withSelectionOnly);
            if (esriLayers == null)
                // return null of array is null
                return null;
            else
            {
                // determine if array contains at least one member
                if (esriLayers.Length > 0)
                    // return the 1st member in array
                    return esriLayers[0];
                else
                    // empty array, return null
                    return null;
            }
        }


        /// <summary>This method is used to return layers based on their underlying database table name.</summary>
        /// <param name='tableName'>Table name that will be used to filter returned layers.</param>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <returns>An array of layer objects</returns>
        public static ILayer[] GetAllLayersByTableName(string tableName, IMap esriMap)
        {
            // get all layers in the map
            System.Collections.ArrayList tempArrayList = EsriUtils.getAllLayers(esriMap, false);
            // create an empty array list to place layers that match criteria
            System.Collections.ArrayList finalArrayList = new System.Collections.ArrayList();
            if (tempArrayList != null)
            {
                // enumerate through array list and do a string compare on source table name and input string
                System.Collections.IEnumerator tempArrayEnumerator = tempArrayList.GetEnumerator();
                int layerMatchCount = 0;
                while (tempArrayEnumerator.MoveNext())
                {
                    // get the layer object from array list
                    ILayer esriLayer = (ILayer) tempArrayEnumerator.Current;
                    // return the layer's table name
                    string currentTableName = EsriUtils.GetLayerTableName(esriLayer);
                    // compare it with the input string (case insensitive)
                    if (System.String.Compare(currentTableName, tableName, true) == 0)
                    {
                        // when strings match, add to other array list and increment counter
                        finalArrayList.Add(esriLayer);
                        layerMatchCount++;
                    }
                }
                // clear the array list containing all the map's layers
                tempArrayList.Clear();
                tempArrayEnumerator = null;
                tempArrayList = null;
            }
            // now transform array list into an array and return it
            return EsriUtils.layerArrayListToArray(finalArrayList);
        }


        /// <summary>This method is used to return one layer based on it's underlying database table name.</summary>
        /// <param name='tableName'>Table name that will be used to filter returned layers.</param>
        /// <param name='esriMap'>A map which contains the layers to be returned.</param>
        /// <returns>An esri ILayer object</returns>
        public static ILayer GetOneLayerByTableName(string tableName, IMap esriMap)
        {
            // get array of all layers based on the input table name
            ILayer[] esriLayers = EsriUtils.GetAllLayersByTableName(tableName, esriMap);
            if (esriLayers == null)
                // return null of array is null
                return null;
            else
            {
                // determine if array contains at least one member
                if (esriLayers.Length > 0)
                    // return the 1st member in array
                    return esriLayers[0];
                else
                    // empty array, return null
                    return null;
            }
        }

        #endregion

        #region functions for manipulating group layers

        /// <summary>
        /// Gets any child layers belonging to the input layer, if it happens to be a group layer
        /// </summary>
        /// <param name="esriLayer">An ESRI ILayer object</param>
        /// <param name="includeContainerObjects">if set to <c>true</c> container objects such as layers that implement IGroupLayer will be excluded</param>
        /// <returns>An array of ILayer objects or null</returns>
        public static ILayer[] GetChildLayers(ILayer esriLayer, bool includeContainerObjects)
        {
            System.Collections.ArrayList esriLayerArrayList = new System.Collections.ArrayList();
            if (esriLayer is IGroupLayer)
            {
                esriLayerArrayList = EsriUtils.addLayerToArrayList(esriLayer, includeContainerObjects,
                    esriLayerArrayList);
                return EsriUtils.layerArrayListToArray(esriLayerArrayList);
            }
            return null;
        }

        #endregion

        #region functions for manipulating individual layers

        /// <summary>This method returns the underlying database table name for a layer</summary>
        /// <param name='esriLayer'>An ESRI ILayer object</param>
        /// <returns>A string representing the layers table name</returns>
        public static string GetLayerTableName(ILayer esriLayer)
        {
            // initialize return value to null
            string layerTableName = null;
            // ensure that layer has a valid connection to it's underlying data and thus can be used safely
            if (EsriUtils.LayerIsValid(esriLayer) == true)
            {
                // ensure this layer supports the required interfaces 
                if (esriLayer is IDataLayer2)
                {
                    // getting the table name for raster & vector data happens two 
                    // ways; determine what we have and call the appropriate code.
                    if (esriLayer is IRasterLayer)
                    {
                        // cast to raster layer and call helper routine to get raster table name
                        IRasterLayer esriRasterLayer = (IRasterLayer) esriLayer;
                        layerTableName = EsriUtils.getRasterTableName(esriRasterLayer);
                    }
                    else
                    {
                        // get vector layer's table name
                        IDataLayer2 esriDataLayer2 = (IDataLayer2) esriLayer;
                        IDatasetName esriDatasetName = (IDatasetName) esriDataLayer2.DataSourceName;
                        layerTableName = esriDatasetName.Name;
                    }
                }
            }
            // return result or initial value of null
            return layerTableName;
        }

        /// <summary>This method returns the geometry field name for a given layer</summary>
        /// <param name='esriLayer'>An ESRI ILayer object</param>
        /// <returns>A string containing the geometry field name</returns>
        public static string GetGeometryFieldName(ILayer esriLayer)
        {
            string geometryFieldName = null;
            if (EsriUtils.LayerIsValid(esriLayer) == true)
            {
                if (esriLayer is IFeatureLayer)
                {
                    try
                    {
                        IFeatureLayer featureLayer = (IFeatureLayer) esriLayer;
                        IFeatureClass featureClass = featureLayer.FeatureClass;
                        return featureClass.ShapeFieldName;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return geometryFieldName;
        }

        /// <summary>This method determines if a layer reference is null or has a valid connection to it's underlying data</summary>
        /// <param name='esriLayer'>An ESRI ILayer object</param>
        /// <returns>A boolean indicating <c>true</c> if layer is valid or <c>false</c> if it is not</returns>
        public static bool LayerIsValid(ILayer esriLayer)
        {
            if (esriLayer == null)
                return false;
            else
            {
                if (esriLayer.Valid == true)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Loads a layerfile into the supplied ESRI map control at index 0 and returns a reference to the layer
        /// </summary>
        /// <param name="fullPath">Path to the layerfile</param>
        /// <param name="mapControl">An ESRI map control object</param>
        /// <returns>An ESRI ILayer object</returns>
        public static ILayer LoadLayerFile(string fullPath, IMapControl2 mapControl)
        {
            if (System.IO.File.Exists(fullPath) == false)
                return null;

            try
            {
                mapControl.AddLayerFromFile(fullPath, 0);
                return mapControl.get_Layer(0);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Creates a clone of the input ESRI layer
        /// </summary>
        /// <param name="inputEsriLayer">An ESRI ILayer object which is to be cloned</param>
        /// <returns>A new ESRI ILayer object which is a clone of the input</returns>
        public static ILayer CloneLayer(ILayer inputEsriLayer)
        {
            if (inputEsriLayer == null)
                return null;

            if (inputEsriLayer is IPersistStream == false)
                return null;

            ILayer clone = (ILayer) EsriUtils.DeepClone((IPersistStream) inputEsriLayer);
            return clone;
        }

        /// <summary>
        /// Clones any object the implements the ESRI interface IPersistStream
        /// </summary>
        /// <param name="input">The input IPersistStream object to be cloned</param>
        /// <returns>A new object which is a clone of the input</returns>
        private static IPersistStream DeepClone(IPersistStream input)
        {
            IObjectCopy objectCopy = new ObjectCopy();
            return (IPersistStream) objectCopy.Copy(input);
        }

        #endregion

        #region functions for manipulating ESRI collection types

        /// <summary>This method determines if a propertyset contains a specific name/value pair based on the supplied name.</summary>
        /// <param name='esriPropertySet'>An ESRI PropertySet object.</param>
        /// <param name='propertyName'>A string containing the name of a property in the set.</param>
        /// <param name='ignoreCase'>if set to <c>true</c> the string's case will be taken into account during search</param>
        /// <returns>A boolean indicating if the property was found in the propertset</returns>
        public static bool PropertyExistsInSet(IPropertySet esriPropertySet, string propertyName, bool ignoreCase)
        {
            if (esriPropertySet != null)
            {
                // use generic object types as out params for GetAllProperties function
                System.Object objectNames;
                System.Object objectValues;
                esriPropertySet.GetAllProperties(out objectNames, out objectValues);
                // cast returned array to arraylist
                System.Collections.ArrayList arrayNames = new System.Collections.ArrayList((System.Array) objectNames);
                System.Collections.ArrayList arrayValues = new System.Collections.ArrayList((System.Array) objectValues);
                // loop through searching for property name
                for (int loopIndex = 0; loopIndex < arrayNames.Count; loopIndex++)
                {
                    if (System.String.Compare(arrayNames[loopIndex].ToString(), propertyName, ignoreCase) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region functions for performing graphics related operations

        /// <summary>
        /// Deletes all graphic elements in a view
        /// </summary>
        /// <param name="activeView">An ESRI IActiveView object</param>
        public static void DeleteAllGraphicElements(IActiveView activeView)
        {
            if (activeView != null)
            {
                activeView.GraphicsContainer.DeleteAllElements();
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }


        /// <summary>This function draws a geometry on a map as a new graphic element</summary>
        /// <param name='sourceGeometry'>An ESRI IGeometry object (currently limited to those implementing IPoint, IPolygon or ICurve).</param>
        /// <param name='esriSymbol'>An ESRI ISymbol object that controls how the geometry will be drawn.</param>
        /// <param name='targetMap'>The IMap that will be drawn to.</param>
        /// <param name='graphicProperties'>An arbitrary IPropertySet that caller can stuff anything into</param>
        /// <param name='graphicName'>A string that serves as the graphic element's name</param>
        public static void AddGeometryAsGraphicToMap(IGeometry sourceGeometry, ISymbol esriSymbol, IMap targetMap,
            IPropertySet graphicProperties, string graphicName)
        {
            if (EsriUtils.GeometryIsValid(sourceGeometry) == false)
                return;

            IElement graphicElement;
            // create marker element for IPoint
            if (sourceGeometry is IPoint)
            {
                graphicElement = new MarkerElement();
                graphicElement.Geometry = sourceGeometry;
                IMarkerElement markerElement = (IMarkerElement) graphicElement;
                markerElement.Symbol = (IMarkerSymbol) esriSymbol;
            }
            // create fill element for IPolygon
            else if (sourceGeometry is IPolygon)
            {
                graphicElement = new PolygonElement();
                graphicElement.Geometry = sourceGeometry;
                IFillShapeElement fillElement = (IFillShapeElement) graphicElement;
                fillElement.Symbol = (IFillSymbol) esriSymbol;
            }
            // create line element for ICurve
            else if (sourceGeometry is ICurve)
            {
                graphicElement = new LineElement();
                graphicElement.Geometry = sourceGeometry;
                ILineElement lineElement = (ILineElement) graphicElement;
                lineElement.Symbol = (ILineSymbol) esriSymbol;
            }
            // catch all for anything else
            else
            {
                return;
            }

            // now add the element to the active view
            if (graphicElement != null)
            {
                // add any custom properties or name that has been associated with this element
                IElementProperties2 elementProperties = (IElementProperties2) graphicElement;
                if (graphicName != null)
                {
                    elementProperties.Name = graphicName;
                }
                if (graphicProperties != null)
                {
                    elementProperties.CustomProperty = graphicProperties;
                }

                // put it on the map
                IActiveView activeView = (IActiveView) targetMap;
                activeView.GraphicsContainer.AddElement(graphicElement, 0);
            }
        }


        /// <summary>This method is used to return a symbol from a server style gallery file.</summary>
        /// <param name='styleFile'>Full path and name of the style file.</param>
        /// <param name='styleClass'>Symbol class such as "Fill Symbols" or "Marker Symbols" as shown in the left-hand pane of the ArcMap style manager.</param>
        /// <param name='styleName'>Symbol name as shown in the right-hand pane of ths ArcMap style manager.</param>
        /// <param name='styleCategory'>Symbol category as shown in the right-hand pane of the ArcMap style manager.  If this parameter is null or empty, it is changed to "Default".</param>
        /// <returns>An ESRI ISymbol object</returns>
        /// <remarks>
        /// To create a server style file, use the ArcMap style manager to create a new style; the style file must be stored in the
        /// ArcGIS\Styles folder.  Next execute the file <i>MakeServerStyleSet.exe</i> which is a tool installed with the ArcObjects
        /// SDK.  This is typically found in the ArcGIS\DeveloperKit\Tools folder.
        /// </remarks>
        public static ISymbol LoadStyleGallerySymbol(string styleFile, string styleClass, string styleName,
            string styleCategory)
        {
            if (System.IO.File.Exists(styleFile) == false)
                return null;

            if (styleCategory == null || styleCategory == "")
                styleCategory = "Default";

            IStyleGallery styleGallery = new ServerStyleGallery();
            IStyleGalleryStorage styleGalleryStorage = (IStyleGalleryStorage) styleGallery;
            styleGalleryStorage.AddFile(styleFile);

            IEnumStyleGalleryItem enumStyleGallery = styleGallery.get_Items(styleClass, styleFile, styleCategory);
            enumStyleGallery.Reset();
            IStyleGalleryItem styleItem = enumStyleGallery.Next();
            while (styleItem != null)
            {
                if (System.String.Compare(styleItem.Name, styleName, true) == 0)
                {
                    if (styleItem.Item is ISymbol)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(enumStyleGallery);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(styleGallery);
                        return (ISymbol) styleItem.Item;
                    }
                }
                styleItem = enumStyleGallery.Next();
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(enumStyleGallery);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(styleGallery);
            return null;
        }

        #endregion

        #region functions for preforming topological operations

        /// <summary>This method takes any geometry and creates an XML representation of it, which can be persisted and later re-hydrated</summary>
        /// <param name='inputGeometry'>An IGeometry object</param>
        /// <returns>A string containing the XML or null if an error is encountered.</returns>
        public static string SerializeGeometryToXML(IGeometry inputGeometry)
        {
            if (EsriUtils.GeometryIsValid(inputGeometry) == false)
                return null;

            try
            {
                IXMLSerializer xmlSerializer = new XMLSerializer();
                return xmlSerializer.SaveToString(inputGeometry, null, null);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>This method evaluates the validity of an ESRI IGeometry object. It is tested for being NULL or containing EMPTY geoemtry</summary>
        /// <param name='esriGeometry'>An ESRI IGeometry object</param>
        /// <returns>A boolean indicating <c>true</c> if geometry is valid or <c>false</c> if it is not</returns>
        public static bool GeometryIsValid(IGeometry esriGeometry)
        {
            if (esriGeometry == null)
                return false;
            else
            {
                if (esriGeometry.IsEmpty == true)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>Ccreates a union of all geometries contained in the supplied cursor</summary>
        /// <param name='featureCursor'>An ESRI IFeatureCursor which contains 1 or more rows</param>
        /// <returns>An IGeometry containing the union or null if an error is encountered</returns>
        public static IGeometry UnionGeometries(IFeatureCursor featureCursor)
        {
            try
            {
                IFeature[] featureArray = EsriUtils.FeatureCursorToArray(featureCursor);
                return EsriUtils.UnionGeometries(featureArray);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>Creates a union of all geometries contained in the array of IFeatures</summary>
        /// <param name='featureArray'>An array of IFeature objects</param>
        /// <returns>An IGeometry containing the union</returns>
        public static IGeometry UnionGeometries(IFeature[] featureArray)
        {
            if (featureArray.Length == 0)
                return null;

            //TODO: appropriate error handling

            IGeometry outputGeometry = null;
            ITopologicalOperator topoOperator = null;
            bool originGeometryFound = false;

            for (int i = 0; i < featureArray.Length; i++)
            {
                IFeature currentFeature = featureArray[i];
                IGeometry currentGeometry = currentFeature.ShapeCopy;
                if (EsriUtils.GeometryIsValid(currentGeometry) == true)
                {
                    if (originGeometryFound == false)
                    {
                        outputGeometry = currentFeature.ShapeCopy;
                        originGeometryFound = true;
                    }
                    else
                    {
                        topoOperator = (ITopologicalOperator) outputGeometry;
                        outputGeometry = topoOperator.Union(currentGeometry);
                    }
                }
            }
            return outputGeometry;
        }

        /// <summary>This method calculates a buffer in feet of the input feature</summary>
        /// <param name='inputFeature'>An IFeature object to be buffered</param>
        /// <returns>An IGeometry containing the buffer or null if an error occurs</returns>
        public static IGeometry CalculateBuffer(IFeature inputFeature, double distanceInFeet)
        {
            if (inputFeature == null)
                return null;
            IGeometry inputGeometry = inputFeature.ShapeCopy;
            try
            {
                return EsriUtils.CalculateBuffer(inputGeometry, distanceInFeet);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>This method calculates a buffer in feet of the input geometry</summary>
        /// <param name='inputGeometry'>An IGeometry object to be buffered</param>
        /// <param name="distanceInFeet">A double representing the buffer radius, may be negative for creating inside buffers of polygons</param>
        /// <returns>An IGeometry containing the buffer or null if something goes wrong</returns>
        public static IGeometry CalculateBuffer(IGeometry inputGeometry, double distanceInFeet)
        {
            if ((EsriUtils.GeometryIsValid(inputGeometry) == false) || (distanceInFeet == 0) ||
                (inputGeometry is ITopologicalOperator == false))
                return null;

            // only allow negative buffers (inside buffers) on polygon geometries
            if (distanceInFeet < 0)
            {
                if ((inputGeometry is IPolygon) == false)
                {
                    return null;
                }
            }

            try
            {
                ITopologicalOperator topoOperator = (ITopologicalOperator) inputGeometry;
                return topoOperator.Buffer(distanceInFeet);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region functions for manipulating workspaces and the objects in them

        #region Functions to open Enterprise Geodatabase Workspace (3 Overloads)

        /// <summary>
        /// Opens an enterprise geodatabase (ArcSDE) workspace (3 Overloads)
        /// </summary>
        /// <param name="propertySet">An ESRI IPropertySet containing connection parameters</param>
        /// <returns>An ESRI IWorkspace object</returns>
        /// <remarks>Will re-throw any errors raised by the ESRI IWorkspaceFactory.Open() method</remarks>
        public static IWorkspace OpenWorkspace(IPropertySet propertySet)
        {
            SdeWorkspaceFactory sdeWorkspaceFactory = new SdeWorkspaceFactory();
            try
            {
                IWorkspace sdeWorkspace = sdeWorkspaceFactory.Open(propertySet, 0);
                return sdeWorkspace;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Opens an enterprise geodatabase (ArcSDE) workspace (3 Overloads)
        /// </summary>
        /// <param name="server">server name or IP address</param>
        /// <param name="instance">SDE instance name or listening port number</param>
        /// <param name="database">database name</param>
        /// <param name="version">geodatabase version name, if null or empty "sde.DEFAULT" is used</param>
        /// <param name="user">user name</param>
        /// <param name="password">user password</param>
        /// <returns>An ESRI IWorkspace object</returns>
        /// <remarks>Will re-throw any errors raised by the ESRI IWorkspaceFactory.Open() method</remarks>
        public static IWorkspace OpenWorkspace(string server, string instance, string database, string version,
            string user, string password)
        {
            if (version == null || version == "")
                version = "sde.DEFAULT";

            PropertySet esriPropertySet = new PropertySet();
            esriPropertySet.SetProperty("SERVER", server);
            esriPropertySet.SetProperty("INSTANCE", instance);
            esriPropertySet.SetProperty("DATABASE", database);
            esriPropertySet.SetProperty("VERSION", version);
            esriPropertySet.SetProperty("USER", user);
            esriPropertySet.SetProperty("PASSWORD", password);
            try
            {
                return EsriUtils.OpenWorkspace(esriPropertySet);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Opens an enterprise geodatabase (ArcSDE) workspace (3 Overloads)
        /// </summary>
        /// <param name="sdeConnectionFile">The full path to an sde connection file (.sde file)</param>
        /// <returns>An ESRI IWorkspace object</returns>
        /// <remarks>Will re-throw any errors raised by the ESRI IWorkspaceFactory.Open() method or a generic <c>System.Exception</c> if the sde connection file was not found</remarks>
        public static IWorkspace OpenWorkspace(string sdeConnectionFile)
        {
            if (System.IO.File.Exists(sdeConnectionFile) == true)
            {
                SdeWorkspaceFactory sdeWorkspaceFactory = new SdeWorkspaceFactory();
                try
                {
                    IWorkspace sdeWorkspace = sdeWorkspaceFactory.OpenFromFile(sdeConnectionFile, 0);
                    return sdeWorkspace;
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new System.Exception("SDE connection file '" + sdeConnectionFile + "' does not exist.");
            }
        }

        #endregion

        /// <summary>
        /// Opens an ESRI geodatabase featureclass
        /// </summary>
        /// <param name="featureClassTableName">Fully qualified name of the database table that a featureclass is stored in</param>
        /// <param name="sourceWorkspace">An ESRI IWorkspaceObject</param>
        /// <returns>An ESRI IFeatureClass object</returns>
        /// <remarks>Will re-throw any errors raised by the ESRI IFeatureWorkspace.OpenFeature() method or a generic <c>System.Exception</c> if the input IWorkspace is null or fails to support IFeatureWorkspace</remarks>
        public static IFeatureClass OpenFeatureClass(string featureClassTableName, IWorkspace sourceWorkspace)
        {
            if (sourceWorkspace != null)
            {
                if (sourceWorkspace is IFeatureWorkspace)
                {
                    IFeatureWorkspace featureWorkspace = (IFeatureWorkspace) sourceWorkspace;
                    try
                    {
                        IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(featureClassTableName);
                        return featureClass;
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new System.Exception("Failed to open featureclass '" + featureClassTableName +
                                               "', the workspace object provided does not support IFeatureWorkspace");
                }
            }
            else
            {
                throw new System.Exception("Failed to open featureclass '" + featureClassTableName +
                                           "', the workspace object provided was null.");
            }
        }


        /// <summary>
        /// Opens an ESRI geodatabase locater 
        /// </summary>
        /// <param name="locaterName">Fully qualified name of the database table that a locater is stored in</param>
        /// <param name="sourceWorkspace">An ESRI IWorkspace object</param>
        /// <returns>An ESRI ILocater object</returns>
        /// <remarks>Will re-throw any errors raised by the ESRI ILocaterWorkspace.GetLocater() method or a generic <c>System.Exception</c> if the input IWorkspace is null</remarks>
        public static ILocator OpenLocater(string locaterName, IWorkspace sourceWorkspace)
        {
            return null;
            //if (sourceWorkspace != null)
            //{
            //    try
            //    {
            //        ILocatorManager2 locaterManager = new LocatorManager();
            //        ILocatorWorkspace locaterWorkspace = locaterManager.GetLocatorWorkspace(sourceWorkspace);
            //        return locaterWorkspace.GetLocator(locaterName);
            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //}
            //else
            //{
            //    throw new System.Exception("Failed to open locater '" + locaterName + "', the workspace object provided was null.");
            //}
        }

        #endregion

        #region functions for manipulating map extent and scale

        /// <summary>
        /// Evaluates if the input geometry is visible within the input map's extent
        /// </summary>
        /// <param name="geometry">An ESRI IGeometry object.</param>
        /// <param name="map">An ESRI IMap object</param>
        /// <returns>A boolean indicating <c>true</c> if geometry is visible within extent or <c>false</c> if it is not</returns>
        public static bool GeometryIsVisible(IGeometry geometry, IMap map)
        {
            if ((EsriUtils.GeometryIsValid(geometry) == false) || map == null)
                return false;
            try
            {
                IActiveView activeView = (IActiveView) map;
                IEnvelope mapEnvelope = activeView.Extent;
                IRelationalOperator relationalOperator = (IRelationalOperator) mapEnvelope;
                return relationalOperator.Contains(geometry);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Centers the input map's extent on the input geometry
        /// </summary>
        /// <param name="geometry">An ESRI IGeometry object</param>
        /// <param name="map">An ESRI IMap object</param>
        /// <param name="mapScale">A number that if not 0 will become the new map scale.  Scale is a representative fraction, where "500" will be interpreted as "1:500"</param>
        public static void CenterOnGeometry(IGeometry geometry, IMap map, double mapScale)
        {
            if ((EsriUtils.GeometryIsValid(geometry) == false) || map == null)
                return;

            IActiveView activeView = (IActiveView) map;
            IEnvelope mapEnvelope = activeView.Extent;
            IEnvelope geometryEnvelope = geometry.Envelope;
            IArea envelopeArea = (IArea) geometryEnvelope;
            mapEnvelope.CenterAt(envelopeArea.Centroid);
            activeView.Extent = mapEnvelope;
            if (mapScale != 0)
            {
                map.MapScale = mapScale;
            }
        }


        /// <summary>
        /// Zooms the input map's extent to the envelope of the input geometry
        /// </summary>
        /// <param name="geometry">An ESRI IGeometry object</param>
        /// <param name="map">An ESRI IMap object</param>
        /// <param name="mapScale">A number that if not 0 will expand the extent in a multiplicative fashion</param>
        public static void ZoomToGeometry(IGeometry geometry, IMap map, double expansionFactor)
        {
            if ((EsriUtils.GeometryIsValid(geometry) == false) || map == null)
                return;
            IEnvelope envelope;
            IActiveView activeView = map as IActiveView;
            if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                if (activeView.ScreenDisplay.DisplayTransformation.ScaleRatio > 1000)
                {
                    activeView.ScreenDisplay.DisplayTransformation.ScaleRatio = 1000;
                }
                envelope = activeView.Extent;
                envelope.CenterAt((IPoint) geometry);
            }
            else
            {
                envelope = geometry.Envelope;
                envelope.Expand(expansionFactor, expansionFactor, true);
            }
            //if (!GeometryUtility.IsCompatibleEnvlope(activeView.Extent, envelope))
            //{
                activeView.Extent = envelope;
                activeView.Refresh();
            //}
        }

        #endregion

        #endregion

        #region  private, utility functions called by this class

        /// <summary>
        /// Gets the the underlying file/table that a IRasterLayer is hydrated from
        /// </summary>
        /// <param name="esriRasterLayer">An ESRI IRasterLayer object</param>
        /// <returns>A string</returns>
        private static string getRasterTableName(IRasterLayer esriRasterLayer)
        {
            string rasterTableName = null;

            try
            {
                IRaster esriRaster = esriRasterLayer.Raster;
                IRasterBandCollection esriRasterBands = (IRasterBandCollection) esriRaster;
                IEnumRasterBand esriRasterBandEnum = esriRasterBands.Bands;

                esriRasterBandEnum.Reset();
                IRasterBand esriRasterBand = esriRasterBandEnum.Next();
                while (esriRasterBand != null)
                {
                    IRasterDataset esriRasterDataset = esriRasterBand.RasterDataset;
                    IDataset esriDataset = (IDataset) esriRasterDataset;
                    rasterTableName = esriDataset.Name;
                    if (rasterTableName != null)
                    {
                        if (rasterTableName != "")
                            break;
                    }
                    esriRasterBand = esriRasterBandEnum.Next();
                }

                if (rasterTableName == "")
                    rasterTableName = null;
            }
            catch
            {
                rasterTableName = null;
            }

            return rasterTableName;
        }

        /// <summary>
        /// Creates a strongly typed array of ILayer objects based on an ArrayList that contains ILayer objects
        /// </summary>
        /// <param name="esriLayerArrayList">An ArrayList containing ILayer objects</param>
        /// <returns>An array of ILayer objects</returns>
        /// <remarks>Any ArrayList members that fail to be cast to ILayer are ignored</remarks>
        private static ILayer[] layerArrayListToArray(System.Collections.ArrayList esriLayerArrayList)
        {
            //HACK: This function shuttles members of an ArrayList into an equally sized array of ILayers
            //for some reason I am unable to cast to the ILayer type when using ArrayList.ToArray() 
            //Fortunately it performs acceptably
            if (esriLayerArrayList != null)
            {
                ILayer[] esriLayerArray = new ILayer[esriLayerArrayList.Count];
                System.Collections.IEnumerator esriLayerEnumerator = esriLayerArrayList.GetEnumerator();
                int esriLayerIndex = 0;
                while (esriLayerEnumerator.MoveNext())
                {
                    try
                    {
                        ILayer esriLayer = (ILayer) esriLayerEnumerator.Current;
                        esriLayerArray[esriLayerIndex] = esriLayer;
                    }
                    catch
                    {
                        // ignore anything that fails cast to ILayer
                    }
                    esriLayerIndex++;
                }
                return esriLayerArray;
            }
            return null;
        }

        /// <summary>
        /// Creates an integer array which represents the object id's of row or feature objects contained in an ESRI ISelectionSet
        /// </summary>
        /// <param name="selectionSet">An ESRI ISelectionSet object</param>
        /// <returns>An array of integers</returns>
        public static int[] SelectionSetToArrayOfIds(ISelectionSet selectionSet)
        {
            if (selectionSet == null)
                return null;

            if (selectionSet.Count == 0)
                return null;

            int[] returnArray = new int[selectionSet.Count];
            int currentID = -1;
            int currentIndex = 0;

            try
            {
                IEnumIDs enumIDs = selectionSet.IDs;
                enumIDs.Reset();
                currentID = enumIDs.Next();
                while (currentID != -1)
                {
                    returnArray[currentIndex] = currentID;
                    currentIndex++;
                    currentID = enumIDs.Next();
                }
                return returnArray;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Creates an array of IFeature objects based on the input ESRI IFeatureCursor
        /// </summary>
        /// <param name="featureCursor">An ESRI IFeatureCursor object</param>
        /// <returns>An array of ESRI IFeature objects</returns>
        public static IFeature[] FeatureCursorToArray(IFeatureCursor featureCursor)
        {
            //HACK: This function will shuttle members of an IFeatureCursor into an array of Features
            //Fortunately it performs acceptably
            if (featureCursor == null)
                return null;

            System.Collections.ArrayList tempArrayList = new System.Collections.ArrayList();
            IFeature esriFeature = featureCursor.NextFeature();
            while (esriFeature != null)
            {
                tempArrayList.Add(esriFeature);
                esriFeature = featureCursor.NextFeature();
            }

            IFeature[] featureArray = new IFeature[tempArrayList.Count];
            System.Collections.IEnumerator arrayEnumerator = tempArrayList.GetEnumerator();
            int esriFeatureIndex = 0;
            while (arrayEnumerator.MoveNext())
            {
                esriFeature = (IFeature) arrayEnumerator.Current;
                featureArray[esriFeatureIndex] = esriFeature;
                esriFeatureIndex++;
            }
            return featureArray;
        }

        /// <summary>
        /// Gets all layers in the input map that implement IDataLayer Container types (group layers) may be excluded
        /// </summary>
        /// <param name="esriMap">An ESRI IMap object</param>
        /// <param name="includeContainerObjects">if set to <c>true</c> container objects such as layers that implement IGroupLayer will be excluded but their children will be included</param>
        /// <returns>An ArrayList containing ILayer objects</returns>
        private static System.Collections.ArrayList getAllLayers(IMap esriMap, bool includeContainerObjects)
        {
            System.Collections.ArrayList esriLayerArrayList = new System.Collections.ArrayList();
            if (esriMap != null)
            {
                int esriLayerIndex = 0;
                int esriLayerCount = esriMap.LayerCount;
                for (esriLayerIndex = 0; esriLayerIndex != esriLayerCount; esriLayerIndex++)
                {
                    ILayer esriLayer = esriMap.get_Layer(esriLayerIndex);
                    if (esriLayer != null)
                    {
                        esriLayerArrayList = addLayerToArrayList(esriLayer, includeContainerObjects, esriLayerArrayList);
                    }
                    esriLayer = null;
                }
            }
            return esriLayerArrayList;
        }

        /// <summary>
        /// Adds an ESRI ILayer to the input ArrayList
        /// </summary>
        /// <param name="esriLayer">An ESRI ILayer object</param>
        /// <param name="includeContainerObjects">if set to <c>true</c> [include container objects].</param>
        /// <param name="inArrayList">An ArrayList which will receive the ILayer object</param>
        /// <returns>An ArrayList</returns>
        /// <remarks>This routine may be called recursively if the layer passed in is an ICompositeLayer or IGroupLayer</remarks>
        private static System.Collections.ArrayList addLayerToArrayList(ILayer esriLayer, bool includeContainerObjects,
            System.Collections.ArrayList inArrayList)
        {
            if (inArrayList == null)
                inArrayList = new System.Collections.ArrayList();

            if (esriLayer != null)
            {
                if (esriLayer is ICompositeLayer)
                {
                    ICompositeLayer esriCompositeLayer = (ICompositeLayer) esriLayer;
                    if (includeContainerObjects == true)
                        inArrayList.Add(esriLayer);

                    int esriChildLayerIndex = 0;
                    int esriChildLayerCount = esriCompositeLayer.Count;
                    for (esriChildLayerIndex = 0; esriChildLayerIndex != esriChildLayerCount; esriChildLayerIndex++)
                    {
                        ILayer esriChildLayer = esriCompositeLayer.get_Layer(esriChildLayerIndex);
                        if (esriChildLayer != null)
                        {
                            EsriUtils.addLayerToArrayList(esriChildLayer, includeContainerObjects, inArrayList);
                        }
                        esriChildLayer = null;
                    }
                    esriCompositeLayer = null;
                }
                else
                {
                    inArrayList.Add(esriLayer);
                }
            }
            return inArrayList;
        }

        #endregion
    }
}